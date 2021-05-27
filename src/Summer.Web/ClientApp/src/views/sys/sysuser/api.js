import request from "@/utils/request";

export function getList(data) {
  return request({
    url: "/sys/sysuser",
    method: "get",
    params: data
  });
}

export function getById(id) {
  return request({
    url: `/sys/sysuser/${id}`,
    method: "get"
  });
}

export function createData(data) {
  return request({
    url: "/sys/sysuser",
    method: "post",
    data: data
  });
}

export function updateData(data) {
  return request({
    url: `/sys/sysuser/${data.id}`,
    method: "put",
    data: data
  });
}

export function deleteData(id) {
  return request({
    url: `/sys/sysuser/${id}`,
    method: "delete"
  });
}
