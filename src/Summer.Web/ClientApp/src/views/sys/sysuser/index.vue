<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button
        class="filter-item"
        type="primary"
        size="small"
        icon="el-icon-edit"
        @click="handleCreate"
      >
        添加 </el-button
      ><el-input
        v-model="listQuery.Query"
        placeholder="查询条件"
        style="width: 200px;"
        class="filter-item"
        size="small"
        @keyup.enter.native="handleFilter"
      />
      <el-button
        class="filter-item"
        type="primary"
        size="small"
        icon="el-icon-search"
        @click="handleFilter"
      >
        查询
      </el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
    >
      <el-table-column label="账号" prop="account" align="center">
        <template slot-scope="{ row }">
          <span>{{ row.account }}</span>
        </template>
      </el-table-column>
      <el-table-column label="姓名" prop="name" align="center">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column label="头像" prop="avatar" align="center">
        <template slot-scope="{ row }">
          <!-- <span class="link-type">{{ row.avatar }}</span> -->
          <img v-if="row.avatar" :src="row.avatar" height="20" />
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        align="center"
        width="230"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row, $index }">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">
            编辑
          </el-button>
          <el-button
            v-if="row.status != 'deleted'"
            size="mini"
            type="danger"
            @click="handleDelete(row, $index)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.PageIndex"
      :limit.sync="listQuery.PageSize"
      @pagination="getList"
    />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-width="100px"
        style="width: 400px; margin-left:50px;"
      >
        <el-form-item label="账号" prop="account">
          <el-input v-model="temp.account" />
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input type="password" v-model="temp.password" />
        </el-form-item>
        <el-form-item label="姓名" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item label="头像" prop="avatar">
          <el-upload
            class="avatar-uploader"
            action="/api/Sys/UploadFile"
            :show-file-list="false"
            :on-success="handleAvatarSuccess"
            :before-upload="beforeAvatarUpload"
          >
            <img v-if="temp.avatar" :src="temp.avatar" class="avatar" />
            <i
              v-else
              class="el-icon-plus avatar-uploader-icon"
              title="请上传人脸照片"
            ></i>
          </el-upload>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button
          type="primary"
          @click="dialogStatus === 'create' ? createData() : updateData()"
        >
          确定
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import { getList, getById, createData, updateData, deleteData } from "./api";

export default {
  name: "sysuser",
  components: { Pagination },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        PageIndex: 1,
        PageSize: 20,
        Query: ""
      },
      temp: {},
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "编辑",
        create: "添加"
      },
      pvData: [],
      rules: {
        account: [
          { required: true, message: "请输入账号", trigger: "blur" },
          { min: 2, message: "长度最少为 2 个字符", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          { min: 6, message: "长度最少为 6 个字符", trigger: "blur" }
        ],
        name: [{ required: true, message: "请输入姓名", trigger: "blur" }],
        avatar: [{ required: true, message: "请上传人脸照片", trigger: "blur" }]
      }
    };
  },
  created() {
    this.getList();
  },
  methods: {
    handleAvatarSuccess(res, file) {
      console.log(res)
      if (res.code === 1) {
        this.temp.avatar = res.data.fileUrl;
        console.log(this.temp.avatar)
      } else {
        this.temp.avatar = "";
      }
      //this.temp.avatar = URL.createObjectURL(file.raw);
    },
    beforeAvatarUpload(file) {
      const isIMG = file.type === "image/jpeg" || file.type === "image/png";
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isIMG) {
        this.$message.error("上传头像图片只能是 JPG/PNG 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isIMG && isLt2M;
    },
    async getList() {
      this.listLoading = true;
      const { data } = await getList(this.listQuery);

      this.total = data.total;
      this.list = data.list;
      this.listLoading = false;
    },
    handleFilter() {
      this.listQuery.PageIndex = 1;
      this.getList();
    },
    resetTemp() {
      this.temp = {
        id: null,
        account: "",
        password: "",
        name: "",
        avatar: ""
      };
    },
    handleCreate() {
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    createData() {
      this.$refs["dataForm"].validate(async valid => {
        if (!valid) return;

        const { message } = await createData(this.temp);
        this.dialogFormVisible = false;
        this.$notify({
          message: message,
          type: "success",
          title: "提示"
        });

        await this.getList();
      });
    },
    async handleUpdate(row) {
      const { data } = await getById(row.id);
      this.temp = data;

      this.dialogStatus = "update";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    updateData() {
      this.$refs["dataForm"].validate(async valid => {
        if (!valid) return;

        const { message } = await updateData(this.temp);
        this.dialogFormVisible = false;
        this.$notify({
          message: message,
          type: "success",
          title: "提示"
        });

        await this.getList();
      });
    },
    handleDelete(row, index) {
      this.$confirm("此操作不可恢复, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(async () => {
        const { message } = await deleteData(row.id);
        this.$notify({
          message: message,
          type: "success",
          title: "提示"
        });

        await this.getList();
      });
    }
  }
};
</script>
