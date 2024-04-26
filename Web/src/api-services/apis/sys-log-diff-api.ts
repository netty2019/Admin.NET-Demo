/* tslint:disable */
/* eslint-disable */
/**
 * Admin.NET 通用权限开发平台
 * 让 .NET 开发更简单、更通用、更流行。整合最新技术，模块插件式开发，前后端分离，开箱即用。<br/><u><b><font color='FF0000'> 👮不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！</font></b></u>
 *
 * OpenAPI spec version: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

import globalAxios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { Configuration } from '../configuration';
// Some imports not used depending on template conditions
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from '../base';
import { AdminResultBoolean } from '../models';
import { AdminResultSqlSugarPagedListSysLogDiff } from '../models';
import { PageLogInput } from '../models';
/**
 * SysLogDiffApi - axios parameter creator
 * @export
 */
export const SysLogDiffApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 清空差异日志 🔖
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiSysLogDiffClearPost: async (options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/sysLogDiff/clear`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication Bearer required
            // http bearer authentication required
            if (configuration && configuration.accessToken) {
                const accessToken = typeof configuration.accessToken === 'function'
                    ? await configuration.accessToken()
                    : await configuration.accessToken;
                localVarHeaderParameter["Authorization"] = "Bearer " + accessToken;
            }

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @summary 获取差异日志分页列表 🔖
         * @param {PageLogInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiSysLogDiffPagePost: async (body?: PageLogInput, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/sysLogDiff/page`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication Bearer required
            // http bearer authentication required
            if (configuration && configuration.accessToken) {
                const accessToken = typeof configuration.accessToken === 'function'
                    ? await configuration.accessToken()
                    : await configuration.accessToken;
                localVarHeaderParameter["Authorization"] = "Bearer " + accessToken;
            }

            localVarHeaderParameter['Content-Type'] = 'application/json-patch+json';

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            const needsSerialization = (typeof body !== "string") || localVarRequestOptions.headers['Content-Type'] === 'application/json';
            localVarRequestOptions.data =  needsSerialization ? JSON.stringify(body !== undefined ? body : {}) : (body || "");

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * SysLogDiffApi - functional programming interface
 * @export
 */
export const SysLogDiffApiFp = function(configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 清空差异日志 🔖
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysLogDiffClearPost(options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<AdminResultBoolean>>> {
            const localVarAxiosArgs = await SysLogDiffApiAxiosParamCreator(configuration).apiSysLogDiffClearPost(options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
        /**
         * 
         * @summary 获取差异日志分页列表 🔖
         * @param {PageLogInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysLogDiffPagePost(body?: PageLogInput, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<AdminResultSqlSugarPagedListSysLogDiff>>> {
            const localVarAxiosArgs = await SysLogDiffApiAxiosParamCreator(configuration).apiSysLogDiffPagePost(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * SysLogDiffApi - factory interface
 * @export
 */
export const SysLogDiffApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        /**
         * 
         * @summary 清空差异日志 🔖
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysLogDiffClearPost(options?: AxiosRequestConfig): Promise<AxiosResponse<AdminResultBoolean>> {
            return SysLogDiffApiFp(configuration).apiSysLogDiffClearPost(options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @summary 获取差异日志分页列表 🔖
         * @param {PageLogInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysLogDiffPagePost(body?: PageLogInput, options?: AxiosRequestConfig): Promise<AxiosResponse<AdminResultSqlSugarPagedListSysLogDiff>> {
            return SysLogDiffApiFp(configuration).apiSysLogDiffPagePost(body, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * SysLogDiffApi - object-oriented interface
 * @export
 * @class SysLogDiffApi
 * @extends {BaseAPI}
 */
export class SysLogDiffApi extends BaseAPI {
    /**
     * 
     * @summary 清空差异日志 🔖
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof SysLogDiffApi
     */
    public async apiSysLogDiffClearPost(options?: AxiosRequestConfig) : Promise<AxiosResponse<AdminResultBoolean>> {
        return SysLogDiffApiFp(this.configuration).apiSysLogDiffClearPost(options).then((request) => request(this.axios, this.basePath));
    }
    /**
     * 
     * @summary 获取差异日志分页列表 🔖
     * @param {PageLogInput} [body] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof SysLogDiffApi
     */
    public async apiSysLogDiffPagePost(body?: PageLogInput, options?: AxiosRequestConfig) : Promise<AxiosResponse<AdminResultSqlSugarPagedListSysLogDiff>> {
        return SysLogDiffApiFp(this.configuration).apiSysLogDiffPagePost(body, options).then((request) => request(this.axios, this.basePath));
    }
}
