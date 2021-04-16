import request from "@/utils/request";

export function login(data) {
  return request({
    url: "/authorization/token",
    method: "post",
    data
  });
}

export function getInfo() {
  return request({
    url: "/sys/sysUser/mine",
    method: "get"
  });
}
