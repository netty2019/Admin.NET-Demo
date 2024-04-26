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

import { EnumEntity } from './enum-entity';
 /**
 * 枚举类型输出参数
 *
 * @export
 * @interface EnumTypeOutput
 */
export interface EnumTypeOutput {

    /**
     * 枚举类型描述
     *
     * @type {string}
     * @memberof EnumTypeOutput
     */
    typeDescribe?: string | null;

    /**
     * 枚举类型名称
     *
     * @type {string}
     * @memberof EnumTypeOutput
     */
    typeName?: string | null;

    /**
     * 枚举类型备注
     *
     * @type {string}
     * @memberof EnumTypeOutput
     */
    typeRemark?: string | null;

    /**
     * 枚举实体
     *
     * @type {Array<EnumEntity>}
     * @memberof EnumTypeOutput
     */
    enumEntities?: Array<EnumEntity> | null;
}
