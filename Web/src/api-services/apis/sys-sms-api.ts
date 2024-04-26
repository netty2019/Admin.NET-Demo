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
/**
 * SysSmsApi - axios parameter creator
 * @export
 */
export const SysSmsApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 发送短信 📨
         * @param {string} phoneNumber 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiSysSmsSendSmsPhoneNumberPost: async (phoneNumber: string, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'phoneNumber' is not null or undefined
            if (phoneNumber === null || phoneNumber === undefined) {
                throw new RequiredError('phoneNumber','Required parameter phoneNumber was null or undefined when calling apiSysSmsSendSmsPhoneNumberPost.');
            }
            const localVarPath = `/api/sysSms/sendSms/{phoneNumber}`
                .replace(`{${"phoneNumber"}}`, encodeURIComponent(String(phoneNumber)));
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
    }
};

/**
 * SysSmsApi - functional programming interface
 * @export
 */
export const SysSmsApiFp = function(configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 发送短信 📨
         * @param {string} phoneNumber 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysSmsSendSmsPhoneNumberPost(phoneNumber: string, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<void>>> {
            const localVarAxiosArgs = await SysSmsApiAxiosParamCreator(configuration).apiSysSmsSendSmsPhoneNumberPost(phoneNumber, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * SysSmsApi - factory interface
 * @export
 */
export const SysSmsApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        /**
         * 
         * @summary 发送短信 📨
         * @param {string} phoneNumber 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiSysSmsSendSmsPhoneNumberPost(phoneNumber: string, options?: AxiosRequestConfig): Promise<AxiosResponse<void>> {
            return SysSmsApiFp(configuration).apiSysSmsSendSmsPhoneNumberPost(phoneNumber, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * SysSmsApi - object-oriented interface
 * @export
 * @class SysSmsApi
 * @extends {BaseAPI}
 */
export class SysSmsApi extends BaseAPI {
    /**
     * 
     * @summary 发送短信 📨
     * @param {string} phoneNumber 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof SysSmsApi
     */
    public async apiSysSmsSendSmsPhoneNumberPost(phoneNumber: string, options?: AxiosRequestConfig) : Promise<AxiosResponse<void>> {
        return SysSmsApiFp(this.configuration).apiSysSmsSendSmsPhoneNumberPost(phoneNumber, options).then((request) => request(this.axios, this.basePath));
    }
}
