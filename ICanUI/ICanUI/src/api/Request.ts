import axios  from 'axios'
import type { AxiosInstance, AxiosError,InternalAxiosRequestConfig,AxiosResponse,AxiosRequestConfig } from 'axios'
import {ElMessage} from 'element-plus'


const NETWORK_ERROR="网络请求异常,请稍后重试......";
const instance:AxiosInstance = axios.create({
  baseURL:"https://localhost:7148/",
  timeout:500
})
instance.interceptors.request.use((config:InternalAxiosRequestConfig)=>{
  const token = localStorage.getItem("token")
   if(token){
    config.headers.Authorization = "Bearer " + token
   }
  return config
},(error:AxiosError)=>{
  return Promise.reject(error)
})

instance.interceptors.response.use((
  response:AxiosResponse
)=>{
const { status, data, statusText} = response
if(status == 200){
  return data
}else if(status == 1000){
  ElMessage.error("没有登录")
}else{
  ElMessage.error(statusText || NETWORK_ERROR)
}
return Promise.reject(new Error(statusText || NETWORK_ERROR))
},(error:AxiosError)=>{
  let message = ""
  let Status= error.response?.status
  if(Status){
    switch(Status){
      case 401:
        message = "token过期"
        break
      case 403:
        message = "无权访问"
        break
      case 404:
        message = "请求地址错误"
        break
      case 500:
        message = "服务器出现问题"
        break
      default:
        message = "其他错误"
    }
  }
  ElMessage.error(message || NETWORK_ERROR) 
  return Promise.reject(error)
})

const http = {
  get<T = any>(url:string,config?:AxiosRequestConfig):Promise<T>{
    return instance.get(url,config)
  },

  post<T=any>(url: string, params?: object, config?: AxiosRequestConfig): Promise<T> {
    return instance.post(url, params, config)
  },
  put<T>(url: string, params?: object): Promise<T> {
    return instance.put(url, params);
  },
  delete<T>(url: string, params?: object): Promise<T> {
    return instance.delete(url, {params});
  }
}
 
 export default http
 