CREATE DATABASE [<<DBName>>] 
GO
use [<<DBName>>]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_IssueRecord]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] DROP CONSTRAINT [FK_IssueAllocation_IssueRecord]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_MaintenanceStatus]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] DROP CONSTRAINT [FK_IssueAllocation_MaintenanceStatus]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [FK_LeaseSignUp_Broker]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [FK_LeaseSignUp_Lawyer]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Broker]'))
ALTER TABLE [LeaseSignUp_Broker] DROP CONSTRAINT [FK_LeaseSignUp_Broker_LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]'))
ALTER TABLE [LeaseSignUp_Lawyer] DROP CONSTRAINT [FK_LeaseSignUp_Lawyer_LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Broker]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Broker]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Lawyer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Lawyer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[uspLeaseSignUp_Payment_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [uspLeaseSignUp_Payment_Insert]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeasePayment]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Charge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeasePayment_Charge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Property]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Property]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SalesActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_SalesActivity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleVisit]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_BrokerGet]'))
DROP VIEW [vwLeaseSignUp_BrokerGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_LawyerGet]'))
DROP VIEW [vwLeaseSignUp_LawyerGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueAllocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_IssueAllocation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_IssueRecord]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Feature]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Feature]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InspectionFeedback]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_InspectionFeedback]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Landlord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Landlord]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LandlordCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LandlordDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Lawyer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Lawyer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BuildingParameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BuildingParameter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Charge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Charge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_City]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_City]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_CompanyDetailType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_CompanyDetailType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Country]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Employee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Employee]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_EmployeeCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_EmployeeDetails]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Broker]'))
ALTER TABLE [LeaseSignUp_Broker] DROP CONSTRAINT [FK_LeaseSignUp_Broker_LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Broker]') AND type in (N'U'))
DROP TABLE [LeaseSignUp_Broker]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ActivityType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ActivityType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Amenity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Amenity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Area]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Area]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Broker]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Broker]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BrokerCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BrokerDetails]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]'))
ALTER TABLE [LeaseSignUp_Lawyer] DROP CONSTRAINT [FK_LeaseSignUp_Lawyer_LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]') AND type in (N'U'))
DROP TABLE [LeaseSignUp_Lawyer]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwSalesActivityCalendarGet]'))
DROP VIEW [vwSalesActivityCalendarGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwSalesActivityGet]'))
DROP VIEW [vwSalesActivityGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeasePayment_ChargeGet]'))
DROP VIEW [vwLeasePayment_ChargeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeasePaymentGet]'))
DROP VIEW [vwLeasePaymentGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyGet]'))
DROP VIEW [vwPropertyGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitCalendarGet]'))
DROP VIEW [vwScheduleVisitCalendarGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitGet]'))
DROP VIEW [vwScheduleVisitGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisitProperty]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleVisitProperty]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_State]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_State]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Tenant]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_TenantCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_TenantDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Zone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Zone]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleTenantVisit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleTenantVisit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestInspection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_RequestInspection]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestInspection_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_RequestInspection_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestMaintenanceCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_RequestMaintenanceCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyCharge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyCharge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyFeature]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyFeature]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyLocationType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyLocationType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PersonalDetailType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PersonalDetailType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyAmenity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyAmenity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyBuildingParameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyBuildingParameter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Tenant]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LocationType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LocationType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_MaintenanceCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_MaintenanceStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerCompanyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LawyerCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LawyerDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Charge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Charge]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwIssueAllocationGet]'))
DROP VIEW [vwIssueAllocationGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwIssueRecordGet]'))
DROP VIEW [vwIssueRecordGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUpGet]'))
DROP VIEW [vwLeaseSignUpGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLocationTypeGet]'))
DROP VIEW [vwLocationTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwMaintenanceCategoryGet]'))
DROP VIEW [vwMaintenanceCategoryGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwMaintenanceStatusGet]'))
DROP VIEW [vwMaintenanceStatusGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPersonalDetailTypeGet]'))
DROP VIEW [vwPersonalDetailTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyAmenityGet]'))
DROP VIEW [vwPropertyAmenityGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyBuildingParameterGet]'))
DROP VIEW [vwPropertyBuildingParameterGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyChargeGet]'))
DROP VIEW [vwPropertyChargeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyFeatureGet]'))
DROP VIEW [vwPropertyFeatureGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordCompanyDetailsGet]'))
DROP VIEW [vwLandlordCompanyDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordDetailsGet]'))
DROP VIEW [vwLandlordDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordGet]'))
DROP VIEW [vwLandlordGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerCompanyDetailsGet]'))
DROP VIEW [vwLawyerCompanyDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerDetailsGet]'))
DROP VIEW [vwLawyerDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerGet]'))
DROP VIEW [vwLawyerGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwActivityTypeGet]'))
DROP VIEW [vwActivityTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwAmenityGet]'))
DROP VIEW [vwAmenityGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwAreaGet]'))
DROP VIEW [vwAreaGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerCompanyDetailsGet]'))
DROP VIEW [vwBrokerCompanyDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerDetailsGet]'))
DROP VIEW [vwBrokerDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerGet]'))
DROP VIEW [vwBrokerGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBuildingParameterGet]'))
DROP VIEW [vwBuildingParameterGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwChargeGet]'))
DROP VIEW [vwChargeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCityGet]'))
DROP VIEW [vwCityGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCompanyDetailTypeGet]'))
DROP VIEW [vwCompanyDetailTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCountryGet]'))
DROP VIEW [vwCountryGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeCompanyDetailsGet]'))
DROP VIEW [vwEmployeeCompanyDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeDetailsGet]'))
DROP VIEW [vwEmployeeDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeGet]'))
DROP VIEW [vwEmployeeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwFeatureGet]'))
DROP VIEW [vwFeatureGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwInspectionFeedbackGet]'))
DROP VIEW [vwInspectionFeedbackGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InvStock]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_InvStock]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceStatus_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_MaintenanceStatus_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceCategory_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_MaintenanceCategory_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LocationType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LocationType_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PersonalDetailType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PersonalDetailType_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyType_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RecordNoSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_RecordNoSettings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Zone_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Zone_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_State_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_State_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SMSCredit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_SMSCredit]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitPropertyGet]'))
DROP VIEW [vwScheduleVisitPropertyGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwStateGet]'))
DROP VIEW [vwStateGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantCompanyDetailsGet]'))
DROP VIEW [vwTenantCompanyDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantDetailsGet]'))
DROP VIEW [vwTenantDetailsGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantGet]'))
DROP VIEW [vwTenantGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwZoneGet]'))
DROP VIEW [vwZoneGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyLocationTypeGet]'))
DROP VIEW [vwPropertyLocationTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyTypeGet]'))
DROP VIEW [vwPropertyTypeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwRequestInspectionGet]'))
DROP VIEW [vwRequestInspectionGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwRequestMaintenanceCategoryGet]'))
DROP VIEW [vwRequestMaintenanceCategoryGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_ChargeGet]'))
DROP VIEW [vwLeaseSignUp_ChargeGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleTenantVisitGet]'))
DROP VIEW [vwScheduleTenantVisitGet]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_TenantGet]'))
DROP VIEW [vwLeaseSignUp_TenantGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SageDB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_SageDB]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_IssueRecord]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] DROP CONSTRAINT [FK_IssueAllocation_IssueRecord]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_MaintenanceStatus]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] DROP CONSTRAINT [FK_IssueAllocation_MaintenanceStatus]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IssueAllocation_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [IssueAllocation] DROP CONSTRAINT [DF_IssueAllocation_CreatedOn]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IssueAllocation]') AND type in (N'U'))
DROP TABLE [IssueAllocation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Area_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Area_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Amenity_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Amenity_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ActivityType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ActivityType_Select]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [FK_LeaseSignUp_Broker]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [FK_LeaseSignUp_Lawyer]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeaseSign__Termi__17F790F9]') AND type = 'D')
BEGIN
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [DF__LeaseSign__Termi__17F790F9]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeaseSign__Creat__18EBB532]') AND type = 'D')
BEGIN
ALTER TABLE [LeaseSignUp] DROP CONSTRAINT [DF__LeaseSign__Creat__18EBB532]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp]') AND type in (N'U'))
DROP TABLE [LeaseSignUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Country_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Country_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_CompanyDetailType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_CompanyDetailType_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_City_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_City_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Charge_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Charge_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BuildingParameter_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BuildingParameter_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Feature_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Feature_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[prcObjectListGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [prcObjectListGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[prcPropertyInformationGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [prcPropertyInformationGet]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Property__Create__2A4B4B5E]') AND type = 'D')
BEGIN
ALTER TABLE [Property] DROP CONSTRAINT [DF__Property__Create__2A4B4B5E]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Property__Status__2B3F6F97]') AND type = 'D')
BEGIN
ALTER TABLE [Property] DROP CONSTRAINT [DF__Property__Status__2B3F6F97]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Property_PropertyFor]') AND type = 'D')
BEGIN
ALTER TABLE [Property] DROP CONSTRAINT [DF_Property_PropertyFor]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Property_PropertyValue]') AND type = 'D')
BEGIN
ALTER TABLE [Property] DROP CONSTRAINT [DF_Property_PropertyValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Property_SalesPersonId]') AND type = 'D')
BEGIN
ALTER TABLE [Property] DROP CONSTRAINT [DF_Property_SalesPersonId]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Property]') AND type in (N'U'))
DROP TABLE [Property]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyAmenity]') AND type in (N'U'))
DROP TABLE [PropertyAmenity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyBuildingParameter]') AND type in (N'U'))
DROP TABLE [PropertyBuildingParameter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyCharge]') AND type in (N'U'))
DROP TABLE [PropertyCharge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyColumns]') AND type in (N'U'))
DROP TABLE [PropertyColumns]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyFeature]') AND type in (N'U'))
DROP TABLE [PropertyFeature]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyLocationType]') AND type in (N'U'))
DROP TABLE [PropertyLocationType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyType]') AND type in (N'U'))
DROP TABLE [PropertyType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RecordNoSettings]') AND type in (N'U'))
DROP TABLE [RecordNoSettings]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RequestIn__Creat__182C9B23]') AND type = 'D')
BEGIN
ALTER TABLE [RequestInspection] DROP CONSTRAINT [DF__RequestIn__Creat__182C9B23]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RequestInspection]') AND type in (N'U'))
DROP TABLE [RequestInspection]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RequestMa__Creat__15502E78]') AND type = 'D')
BEGIN
ALTER TABLE [RequestMaintenanceCategory] DROP CONSTRAINT [DF__RequestMa__Creat__15502E78]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RequestMaintenanceCategory]') AND type in (N'U'))
DROP TABLE [RequestMaintenanceCategory]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SageDB_BatchesId]') AND type = 'D')
BEGIN
ALTER TABLE [SageDB] DROP CONSTRAINT [DF_SageDB_BatchesId]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SageDB]') AND type in (N'U'))
DROP TABLE [SageDB]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__SalesActi__Creat__361203C5]') AND type = 'D')
BEGIN
ALTER TABLE [SalesActivity] DROP CONSTRAINT [DF__SalesActi__Creat__361203C5]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SalesActivity_BriefRemarks]') AND type = 'D')
BEGIN
ALTER TABLE [SalesActivity] DROP CONSTRAINT [DF_SalesActivity_BriefRemarks]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SalesActivity]') AND type in (N'U'))
DROP TABLE [SalesActivity]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ScheduleT__Creat__108B795B]') AND type = 'D')
BEGIN
ALTER TABLE [ScheduleTenantVisit] DROP CONSTRAINT [DF__ScheduleT__Creat__108B795B]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleTenantVisit]') AND type in (N'U'))
DROP TABLE [ScheduleTenantVisit]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ScheduleV__Creat__0DAF0CB0]') AND type = 'D')
BEGIN
ALTER TABLE [ScheduleVisit] DROP CONSTRAINT [DF__ScheduleV__Creat__0DAF0CB0]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleVisit]') AND type in (N'U'))
DROP TABLE [ScheduleVisit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleVisitProperty]') AND type in (N'U'))
DROP TABLE [ScheduleVisitProperty]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SMSTemplateSettings]') AND type in (N'U'))
DROP TABLE [SMSTemplateSettings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[sp_helptext_GSM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [sp_helptext_GSM]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [Split]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[State]') AND type in (N'U'))
DROP TABLE [State]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Tenant__CreatedO__07020F21]') AND type = 'D')
BEGIN
ALTER TABLE [Tenant] DROP CONSTRAINT [DF__Tenant__CreatedO__07020F21]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Tenant_PaymentMode]') AND type = 'D')
BEGIN
ALTER TABLE [Tenant] DROP CONSTRAINT [DF_Tenant_PaymentMode]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tenant]') AND type in (N'U'))
DROP TABLE [Tenant]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantCompanyDetails]') AND type in (N'U'))
DROP TABLE [TenantCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantCompanyInformation]') AND type in (N'U'))
DROP TABLE [TenantCompanyInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantDetails]') AND type in (N'U'))
DROP TABLE [TenantDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantInformation]') AND type in (N'U'))
DROP TABLE [TenantInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_GetData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_GetData]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InspectionFeedback_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_InspectionFeedback_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueRecord_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_IssueRecord_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueAllocation_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_IssueAllocation_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Lawyer_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Lawyer_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LandlordDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordCompanyDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LandlordCompanyDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Landlord_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Landlord_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Employee_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Employee_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_EmployeeDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeCompanyDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_EmployeeCompanyDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Charge]') AND type in (N'U'))
DROP TABLE [LeaseSignUp_Charge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Broker_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Broker_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BrokerDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerCompanyDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_BrokerCompanyDetails_Select]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IssueRecord_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [IssueRecord] DROP CONSTRAINT [DF_IssueRecord_CreatedOn]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IssueRecord]') AND type in (N'U'))
DROP TABLE [IssueRecord]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Landlord__Create__18EBB532]') AND type = 'D')
BEGIN
ALTER TABLE [Landlord] DROP CONSTRAINT [DF__Landlord__Create__18EBB532]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Landlord]') AND type in (N'U'))
DROP TABLE [Landlord]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordCompanyDetails]') AND type in (N'U'))
DROP TABLE [LandlordCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordCompanyInformation]') AND type in (N'U'))
DROP TABLE [LandlordCompanyInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordDetails]') AND type in (N'U'))
DROP TABLE [LandlordDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordInformation]') AND type in (N'U'))
DROP TABLE [LandlordInformation]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Lawyer__CreatedO__0E6E26BF]') AND type = 'D')
BEGIN
ALTER TABLE [Lawyer] DROP CONSTRAINT [DF__Lawyer__CreatedO__0E6E26BF]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Lawyer]') AND type in (N'U'))
DROP TABLE [Lawyer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerCompanyDetails]') AND type in (N'U'))
DROP TABLE [LawyerCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerCompanyInformation]') AND type in (N'U'))
DROP TABLE [LawyerCompanyInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerDetails]') AND type in (N'U'))
DROP TABLE [LawyerDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerInformation]') AND type in (N'U'))
DROP TABLE [LawyerInformation]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LeasePayment_TenantId]') AND type = 'D')
BEGIN
ALTER TABLE [LeasePayment] DROP CONSTRAINT [DF_LeasePayment_TenantId]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeasePaym__Invoi__084B3915]') AND type = 'D')
BEGIN
ALTER TABLE [LeasePayment] DROP CONSTRAINT [DF__LeasePaym__Invoi__084B3915]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeasePaym__Payme__093F5D4E]') AND type = 'D')
BEGIN
ALTER TABLE [LeasePayment] DROP CONSTRAINT [DF__LeasePaym__Payme__093F5D4E]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeasePaym__Termi__0A338187]') AND type = 'D')
BEGIN
ALTER TABLE [LeasePayment] DROP CONSTRAINT [DF__LeasePaym__Termi__0A338187]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__LeasePaym__Creat__0B27A5C0]') AND type = 'D')
BEGIN
ALTER TABLE [LeasePayment] DROP CONSTRAINT [DF__LeasePaym__Creat__0B27A5C0]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeasePayment]') AND type in (N'U'))
DROP TABLE [LeasePayment]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeasePayment_Charge]') AND type in (N'U'))
DROP TABLE [LeasePayment_Charge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Tenant]') AND type in (N'U'))
DROP TABLE [LeaseSignUp_Tenant]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LocationType]') AND type in (N'U'))
DROP TABLE [LocationType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MaintenanceCategory]') AND type in (N'U'))
DROP TABLE [MaintenanceCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MaintenanceStatus]') AND type in (N'U'))
DROP TABLE [MaintenanceStatus]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PersonalDetailType_DetailType]') AND type = 'D')
BEGIN
ALTER TABLE [PersonalDetailType] DROP CONSTRAINT [DF_PersonalDetailType_DetailType]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PersonalDetailType]') AND type in (N'U'))
DROP TABLE [PersonalDetailType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ActivityType]') AND type in (N'U'))
DROP TABLE [ActivityType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Amenity]') AND type in (N'U'))
DROP TABLE [Amenity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Area]') AND type in (N'U'))
DROP TABLE [Area]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Broker__CreatedO__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [Broker] DROP CONSTRAINT [DF__Broker__CreatedO__6FE99F9F]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Broker]') AND type in (N'U'))
DROP TABLE [Broker]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerCompanyDetails]') AND type in (N'U'))
DROP TABLE [BrokerCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerCompanyInformation]') AND type in (N'U'))
DROP TABLE [BrokerCompanyInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerDetails]') AND type in (N'U'))
DROP TABLE [BrokerDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerInformation]') AND type in (N'U'))
DROP TABLE [BrokerInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BuildingParameter]') AND type in (N'U'))
DROP TABLE [BuildingParameter]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Charge]') AND type in (N'U'))
DROP TABLE [Charge]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Charge_PropertyType]') AND type in (N'U'))
DROP TABLE [Charge_PropertyType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[City]') AND type in (N'U'))
DROP TABLE [City]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CompanyDetailType_DetailType]') AND type = 'D')
BEGIN
ALTER TABLE [CompanyDetailType] DROP CONSTRAINT [DF_CompanyDetailType_DetailType]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompanyDetailType]') AND type in (N'U'))
DROP TABLE [CompanyDetailType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Country]') AND type in (N'U'))
DROP TABLE [Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmailTemplateSettings]') AND type in (N'U'))
DROP TABLE [EmailTemplateSettings]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Employee__Create__48CFD27E]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF__Employee__Create__48CFD27E]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_IsSalesPerson]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF_Employee_IsSalesPerson]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_IsPropertyManager]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF_Employee_IsPropertyManager]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_IsOthers]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF_Employee_IsOthers]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_IsCareTacker]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF_Employee_IsCareTacker]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Employee_IsEmployee]') AND type = 'D')
BEGIN
ALTER TABLE [Employee] DROP CONSTRAINT [DF_Employee_IsEmployee]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Employee]') AND type in (N'U'))
DROP TABLE [Employee]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeCompanyDetails]') AND type in (N'U'))
DROP TABLE [EmployeeCompanyDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeCompanyInformation]') AND type in (N'U'))
DROP TABLE [EmployeeCompanyInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeDetails]') AND type in (N'U'))
DROP TABLE [EmployeeDetails]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeInformation]') AND type in (N'U'))
DROP TABLE [EmployeeInformation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Feature]') AND type in (N'U'))
DROP TABLE [Feature]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Inspectio__Creat__3C69FB99]') AND type = 'D')
BEGIN
ALTER TABLE [InspectionFeedback] DROP CONSTRAINT [DF__Inspectio__Creat__3C69FB99]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[InspectionFeedback]') AND type in (N'U'))
DROP TABLE [InspectionFeedback]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[InventoryStock]') AND type in (N'U'))
DROP TABLE [InventoryStock]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Zone]') AND type in (N'U'))
DROP TABLE [Zone]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SMSCredit_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_SMSCredit_Search]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisitProperty_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleVisitProperty_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisit_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleVisit_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_TenantDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantCompanyDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_TenantCompanyDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Tenant_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Tenant_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyLocationType_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyLocationType_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyFeature_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyFeature_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyCharge_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyCharge_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestMaintenanceCategory_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_RequestMaintenanceCategory_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleTenantVisit_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_ScheduleTenantVisit_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SalesActivity_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_SalesActivity_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Property_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_Property_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyBuildingParameter_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyBuildingParameter_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyAmenity_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_PropertyAmenity_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Tenant_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Tenant_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Broker_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Broker_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Charge_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Charge_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Lawyer_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Lawyer_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeaseSignUp_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LawyerDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerCompanyDetails_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LawyerCompanyDetails_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Charge_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeasePayment_Charge_Select]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Select]') AND type in (N'P', N'PC'))
DROP PROCEDURE [usp_LeasePayment_Select]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_LeasePayment_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select LeasePaymentId As [Value], [Lease SignUp No] As [Text] From vwLeasePaymentGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeasePaymentGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Charge_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_LeasePayment_Charge_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LeasePayment_ChargeId As [Value], ChargeName As [Text] From vwLeasePayment_ChargeGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeasePayment_ChargeGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerCompanyDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LawyerCompanyDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwLawyerCompanyDetailsGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LawyerDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwLawyerDetailsGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_LeaseSignUp_Select]
	@Op As nVarchar(Max) = '''',
	@Search As nVarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select LeaseSignUpId As [Value], [Lease SignUp No] As [Text] From vwLeaseSignUpGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeaseSignUpGet Where '' +  @Search)
		End
	Else If @Op = ''PR''
		Begin
			Exec (''Select LeaseSignUpId As [Value], [Lease SignUp No] As [Text] From vwLeaseSignUpGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Lawyer_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [usp_LeaseSignUp_Lawyer_Select]
	@Op As Varchar(Max) = '''',
	@Search As Varchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LeaseSignUp_LawyerId As [Value], LawyerName As [Text] From vwLeaseSignUp_LawyerGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeaseSignUp_LawyerGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Charge_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LeaseSignUp_Charge_Select]
	@Op As Varchar(Max) = '''',
	@Search As Varchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LeaseSignUp_ChargeId As [Value], ChargeName As [Text] From vwLeaseSignUp_ChargeGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeaseSignUp_ChargeGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Broker_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [usp_LeaseSignUp_Broker_Select]
	@Op As Varchar(Max) = '''',
	@Search As Varchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LeaseSignUp_BrokerId As [Value], BrokerName As [Text] From vwLeaseSignUp_BrokerGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeaseSignUp_BrokerGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Tenant_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_LeaseSignUp_Tenant_Select]
	@Op As Varchar(Max) = '''',
	@Search As Varchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LeaseSignUp_TenantId As [Value], TenantName As [Text] From vwLeaseSignUp_TenantGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLeaseSignUp_TenantGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyAmenity_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_PropertyAmenity_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyAmenityGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyBuildingParameter_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyBuildingParameter_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyBuildingParameterGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Property_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Property_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select PropertyId As [Value], [Property LR No] As [Text] From vwPropertyGet Where '' +  @Search + '' Order By [Property LR No] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyGet Where '' +  @Search + '' Order By [Property LR No] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SalesActivity_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_SalesActivity_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select SalesActivityId As [Value], [Property LR No] As [Text] From vwSalesActivityGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwSalesActivityGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleTenantVisit_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_ScheduleTenantVisit_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select ScheduleTenantVisitId As [Value], Name As [Text] From vwScheduleTenantVisitGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwScheduleTenantVisitGet Where '' +  @Search)
		End
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestMaintenanceCategory_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_RequestMaintenanceCategory_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select RequestMaintenanceCategoryId As [Value], Name As [Text] From vwRequestMaintenanceCategoryGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwRequestMaintenanceCategoryGet Where '' +  @Search)
		End
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyCharge_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_PropertyCharge_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyChargeGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyFeature_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyFeature_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyFeatureGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyLocationType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyLocationType_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwPropertyLocationTypeGet Where '' +  @Search)
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Tenant_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Tenant_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select TenantId As [Value], [Tenant Name] As [Text] From vwTenantGet Where '' +  @Search + '' Order By [Tenant Name] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwTenantGet Where '' +  @Search + '' Order By [Tenant Name] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantCompanyDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_TenantCompanyDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwTenantCompanyDetailsGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_TenantDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwTenantDetailsGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisit_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_ScheduleVisit_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select ScheduleVisitId As [Value], Name As [Text] From vwScheduleVisitGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwScheduleVisitGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisitProperty_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_ScheduleVisitProperty_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select ScheduleVisitPropertyId As [Value], [Property LR No] As [Text] From vwScheduleVisitPropertyGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwScheduleVisitPropertyGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SMSCredit_Search]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_SMSCredit_Search]  
	@CompanyId As Varchar(Max) = ''0'',   
	@Search As nvarchar(Max) = ''''  
As  
Begin  
	If @Search = ''''  
		Set @Search = ''1 = 1''  
	If @CompanyId = ''0''  
		Set @CompanyId = ''%''  
  
	Exec (''Select SMSCredit.SMSCreditId, SMSCredit.CreditNoteNo, SMSCredit.TransactionDate, SMSCredit.CompanyId, Company.CompanyName, SMSCredit.Quantity, SMSCredit.RatePerSMS,   
			Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) As GrossAmount, SMSCredit.TaxPercent,   
			Convert(decimal(18, 2), ((Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) * SMSCredit.TaxPercent) / 100)) As TaxAmount,      
			Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) +   
			Convert(decimal(18, 2), ((Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) * SMSCredit.TaxPercent) / 100)) As TotalAmount,   
			SMSCredit.Description, SMSCredit.CreatedOn  
		From SMSCredit Inner Join Company On SMSCredit.CompanyId = Company.CompanyId  
		Where Isnull(SMSCredit.CompanyId, 0) Like '''''' + @CompanyId + '''''' And SMSCredit.DeletedOn Is Null And '' + @Search)  
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Zone]') AND type in (N'U'))
BEGIN
CREATE TABLE [Zone](
	[ZoneId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CityId] [int] NOT NULL,
	[ZoneName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Zone] PRIMARY KEY CLUSTERED 
(
	[ZoneId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[InventoryStock]') AND type in (N'U'))
BEGIN
CREATE TABLE [InventoryStock](
	[ItemId] [bigint] NULL,
	[Item Code] [varchar](max) NULL,
	[Item Name] [varchar](max) NULL,
	[Item Group] [varchar](max) NULL,
	[Qty On Hand] [float] NULL,
	[Mstr] [float] NULL,
	[WL] [float] NULL,
	[NYK] [float] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[InspectionFeedback]') AND type in (N'U'))
BEGIN
CREATE TABLE [InspectionFeedback](
	[InspectionFeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[RequestInspectionId] [int] NULL,
	[TenantId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[VisitDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_InspectionFeedback] PRIMARY KEY CLUSTERED 
(
	[InspectionFeedbackId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Feature]') AND type in (N'U'))
BEGIN
CREATE TABLE [Feature](
	[FeatureId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[FeatureName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [EmployeeInformation](
	[EmployeeId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_EmployeeInformation] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [EmployeeDetails](
	[EmployeeDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeId] [int] NOT NULL,
	[PersonalDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_EmployeeDetails] PRIMARY KEY CLUSTERED 
(
	[EmployeeDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeCompanyInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [EmployeeCompanyInformation](
	[EmployeeId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_EmployeeCompanyInformation] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeCompanyDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [EmployeeCompanyDetails](
	[EmployeeCompanyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeId] [int] NOT NULL,
	[CompanyDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_EmployeeCompanyDetails] PRIMARY KEY CLUSTERED 
(
	[EmployeeCompanyDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[IsSalesPerson] [int] NOT NULL CONSTRAINT [DF_Employee_IsSalesPerson]  DEFAULT ((0)),
	[IsPropertyManager] [int] NOT NULL CONSTRAINT [DF_Employee_IsPropertyManager]  DEFAULT ((0)),
	[IsOthers] [int] NOT NULL CONSTRAINT [DF_Employee_IsOthers]  DEFAULT ((0)),
	[IsCareTacker] [int] NOT NULL CONSTRAINT [DF_Employee_IsCareTacker]  DEFAULT ((0)),
	[IsEmployee] [int] NOT NULL CONSTRAINT [DF_Employee_IsEmployee]  DEFAULT ((0)),
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmailTemplateSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [EmailTemplateSettings](
	[EmailTemplateSettingsId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[EmailTemplateType] [varchar](max) NOT NULL,
	[EmailTemplate] [varchar](max) NOT NULL,
 CONSTRAINT [PK_EmailTemplateSettings] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateSettingsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CountryName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompanyDetailType]') AND type in (N'U'))
BEGIN
CREATE TABLE [CompanyDetailType](
	[CompanyDetailTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeName] [nvarchar](max) NOT NULL,
	[TextMode] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DetailType] [int] NOT NULL CONSTRAINT [DF_CompanyDetailType_DetailType]  DEFAULT ((0)),
 CONSTRAINT [PK_CompanyDetailType] PRIMARY KEY CLUSTERED 
(
	[CompanyDetailTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[StateId] [int] NOT NULL,
	[CityName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Charge_PropertyType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Charge_PropertyType](
	[ChargeId] [bigint] NOT NULL,
	[PropertyTypeId] [bigint] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Charge]') AND type in (N'U'))
BEGIN
CREATE TABLE [Charge](
	[ChargeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ChargeName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Charge] PRIMARY KEY CLUSTERED 
(
	[ChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BuildingParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [BuildingParameter](
	[BuildingParameterId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[BuildingParameterName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_BuildingParameter] PRIMARY KEY CLUSTERED 
(
	[BuildingParameterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [BrokerInformation](
	[BrokerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_BrokerInformation] PRIMARY KEY CLUSTERED 
(
	[BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [BrokerDetails](
	[BrokerDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[BrokerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeId] [int] NOT NULL,
	[PersonalDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_BrokerDetails] PRIMARY KEY CLUSTERED 
(
	[BrokerDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerCompanyInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [BrokerCompanyInformation](
	[BrokerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_BrokerCompanyInformation] PRIMARY KEY CLUSTERED 
(
	[BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BrokerCompanyDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [BrokerCompanyDetails](
	[BrokerCompanyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[BrokerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeId] [int] NOT NULL,
	[CompanyDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_BrokerCompanyDetails] PRIMARY KEY CLUSTERED 
(
	[BrokerCompanyDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Broker]') AND type in (N'U'))
BEGIN
CREATE TABLE [Broker](
	[BrokerId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Broker] PRIMARY KEY CLUSTERED 
(
	[BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Area]') AND type in (N'U'))
BEGIN
CREATE TABLE [Area](
	[AreaId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ZoneId] [int] NOT NULL,
	[AreaName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Amenity]') AND type in (N'U'))
BEGIN
CREATE TABLE [Amenity](
	[AmenityId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[AmenityName] [nvarchar](max) NOT NULL,
	[AmenityType] [smallint] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Amenity] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ActivityType]') AND type in (N'U'))
BEGIN
CREATE TABLE [ActivityType](
	[ActivityTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ActivityTypeName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PersonalDetailType]') AND type in (N'U'))
BEGIN
CREATE TABLE [PersonalDetailType](
	[PersonalDetailTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeName] [nvarchar](max) NOT NULL,
	[TextMode] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DetailType] [int] NOT NULL CONSTRAINT [DF_PersonalDetailType_DetailType]  DEFAULT ((0)),
 CONSTRAINT [PK_PersonalDetailType] PRIMARY KEY CLUSTERED 
(
	[PersonalDetailTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MaintenanceStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [MaintenanceStatus](
	[MaintenanceStatusId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[MaintenanceStatusName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_MaintenanceStatus] PRIMARY KEY CLUSTERED 
(
	[MaintenanceStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MaintenanceCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [MaintenanceCategory](
	[MaintenanceCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[MaintenanceCategoryName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_MaintenanceCategory] PRIMARY KEY CLUSTERED 
(
	[MaintenanceCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LocationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [LocationType](
	[LocationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LocationTypeName] [nvarchar](max) NOT NULL,
	[DataPickFrom] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_LocationType] PRIMARY KEY CLUSTERED 
(
	[LocationTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Tenant]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeaseSignUp_Tenant](
	[LeaseSignUp_TenantId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[TenantPercentage] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_LeaseSignUp_Tenant] PRIMARY KEY CLUSTERED 
(
	[LeaseSignUp_TenantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeasePayment_Charge]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeasePayment_Charge](
	[LeasePayment_ChargeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeasePaymentId] [bigint] NOT NULL,
	[LeaseSignUpId] [bigint] NOT NULL,
	[ChargeId] [bigint] NOT NULL,
	[Amount] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_LeasePayment_Charge] PRIMARY KEY CLUSTERED 
(
	[LeasePayment_ChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeasePayment]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeasePayment](
	[LeasePaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpId] [bigint] NOT NULL,
	[TenantId] [bigint] NOT NULL CONSTRAINT [DF_LeasePayment_TenantId]  DEFAULT ((0)),
	[DueDate] [datetime] NOT NULL,
	[InvoiceStatus] [int] NOT NULL DEFAULT ((0)),
	[InvoiceId] [bigint] NOT NULL,
	[InvoiceDate] [datetime] NULL,
	[PaymentStatus] [int] NOT NULL DEFAULT ((0)),
	[PaymentId] [bigint] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[Comment] [varchar](max) NULL,
	[TerminateFlag] [int] NOT NULL DEFAULT ((0)),
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModIfiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_LeasePayment] PRIMARY KEY CLUSTERED 
(
	[LeasePaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [LawyerInformation](
	[LawyerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_LawyerInformation] PRIMARY KEY CLUSTERED 
(
	[LawyerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [LawyerDetails](
	[LawyerDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[LawyerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeId] [int] NOT NULL,
	[PersonalDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_LawyerDetails] PRIMARY KEY CLUSTERED 
(
	[LawyerDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerCompanyInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [LawyerCompanyInformation](
	[LawyerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_LawyerCompanyInformation] PRIMARY KEY CLUSTERED 
(
	[LawyerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LawyerCompanyDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [LawyerCompanyDetails](
	[LawyerCompanyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[LawyerId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeId] [int] NOT NULL,
	[CompanyDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_LawyerCompanyDetails] PRIMARY KEY CLUSTERED 
(
	[LawyerCompanyDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Lawyer]') AND type in (N'U'))
BEGIN
CREATE TABLE [Lawyer](
	[LawyerId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Lawyer] PRIMARY KEY CLUSTERED 
(
	[LawyerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [LandlordInformation](
	[LandlordId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_LandlordInformation] PRIMARY KEY CLUSTERED 
(
	[LandlordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [LandlordDetails](
	[LandlordDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[LandlordId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeId] [int] NOT NULL,
	[PersonalDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_LandlordDetails] PRIMARY KEY CLUSTERED 
(
	[LandlordDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordCompanyInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [LandlordCompanyInformation](
	[LandlordId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_LandlordCompanyInformation] PRIMARY KEY CLUSTERED 
(
	[LandlordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LandlordCompanyDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [LandlordCompanyDetails](
	[LandlordCompanyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[LandlordId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeId] [int] NOT NULL,
	[CompanyDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_LandlordCompanyDetails] PRIMARY KEY CLUSTERED 
(
	[LandlordCompanyDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Landlord]') AND type in (N'U'))
BEGIN
CREATE TABLE [Landlord](
	[LandlordId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Landlord] PRIMARY KEY CLUSTERED 
(
	[LandlordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IssueRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [IssueRecord](
	[IssueRecordId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[IssueNumber] [varchar](max) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[RecordedBy] [int] NOT NULL,
	[RecordedById] [int] NULL,
	[RecordName] [varchar](max) NULL,
	[RecordMobile] [varchar](max) NULL,
	[RecordEmail] [varchar](max) NULL,
	[RecordDate] [datetime] NULL,
	[CategoryId] [int] NULL,
	[Description] [varchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_IssueRecord_CreatedOn]  DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_IssueRecord] PRIMARY KEY CLUSTERED 
(
	[IssueRecordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerCompanyDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_BrokerCompanyDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwBrokerCompanyDetailsGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_BrokerDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwBrokerDetailsGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Broker_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Broker_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select BrokerId As [Value], [Broker Name] As [Text] From vwBrokerGet Where '' +  @Search + '' Order By [Broker Name] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwBrokerGet Where '' +  @Search + '' Order By [Broker Name] Asc'')
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Charge]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeaseSignUp_Charge](
	[LeaseSignUp_ChargeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpId] [int] NOT NULL,
	[ChargeId] [int] NOT NULL,
	[Mode] [nvarchar](max) NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_LeaseSignUp_Charge] PRIMARY KEY CLUSTERED 
(
	[LeaseSignUp_ChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeCompanyDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_EmployeeCompanyDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwEmployeeCompanyDetailsGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_EmployeeDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwEmployeeDetailsGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Employee_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Employee_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select EmployeeId As [Value], [Employee Name] As [Text] From vwEmployeeGet Where '' +  @Search + '' Order By [Employee Name] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwEmployeeGet Where '' +  @Search + '' Order By [Employee Name] Asc'')
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Landlord_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Landlord_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LandlordId As [Value], [Landlord Name] As [Text] From vwLandlordGet Where '' +  @Search + '' Order By [Landlord Name] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLandlordGet Where '' +  @Search + '' Order By [Landlord Name] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordCompanyDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LandlordCompanyDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwLandlordCompanyDetailsGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordDetails_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LandlordDetails_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''Select''
		Begin
			Exec (''Select * From vwLandlordDetailsGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Lawyer_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Lawyer_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''GD''
		Begin
			Exec (''Select LawyerId As [Value], [Lawyer Name] As [Text] From vwLawyerGet Where '' +  @Search + '' Order By [Lawyer Name] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwLawyerGet Where '' +  @Search + '' Order By [Lawyer Name] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueAllocation_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE Procedure [usp_IssueAllocation_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select IssueAllocationId As [Value], IssueNumber As [Text] From vwIssueAllocationGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwIssueAllocationGet Where '' +  @Search)
		End
End



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueRecord_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_IssueRecord_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select IssueRecordId As [Value], [Record LR No] As [Text] From vwIssueRecordGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwIssueRecordGet Where '' +  @Search)
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InspectionFeedback_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_InspectionFeedback_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select InspectionFeedbackId As [Value], Name As [Text] From vwInspectionFeedbackGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwInspectionFeedbackGet Where '' +  @Search)
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_GetData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_GetData]  
(
	@ViewName Varchar(Max),
	@FieldName Varchar(Max)= Null
)
As
Begin
	Set NoCount On;
	If @FieldName is null
		Begin
			Exec (''Select * From '' + @ViewName)  
		End
	Else
		Begin
			Exec (''Select '' +  @FieldName + '' From '' + @ViewName)  
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [TenantInformation](
	[TenantId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_TenantInformation] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [TenantDetails](
	[TenantDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PersonalDetailTypeId] [int] NOT NULL,
	[PersonalDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TenantDetails] PRIMARY KEY CLUSTERED 
(
	[TenantDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantCompanyInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [TenantCompanyInformation](
	[TenantId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
 CONSTRAINT [PK_TenantCompanyInformation] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TenantCompanyDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [TenantCompanyDetails](
	[TenantCompanyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CompanyDetailTypeId] [int] NOT NULL,
	[CompanyDetailValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_TenantCompanyDetails] PRIMARY KEY CLUSTERED 
(
	[TenantCompanyDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Tenant]') AND type in (N'U'))
BEGIN
CREATE TABLE [Tenant](
	[TenantId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Type] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[PaymentMode] [int] NOT NULL CONSTRAINT [DF_Tenant_PaymentMode]  DEFAULT ((0)),
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[State]') AND type in (N'U'))
BEGIN
CREATE TABLE [State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[CountryId] [int] NOT NULL,
	[StateName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create Function [Split](
	@String varchar(Max), 
	@Delimiter char(1))     
Returns @temptable TABLE 
(
items varchar(Max)
)     
As
Begin
	Declare @idx As Int
	Declare @slice As Varchar(Max)     
	Select @idx = 1     
	if len(@String) < 1 or @String is null  
		Return     
    
	While @idx!= 0     
	Begin     
		set @idx = charindex(@Delimiter,@String)     
		if @idx!=0     
			set @slice = left(@String,@idx - 1)     
		else     
			set @slice = @String     
		
		if(len(@slice)>0)
			insert into @temptable(Items) values(@slice)     

		set @String = right(@String,len(@String) - @idx)     
		if len(@String) = 0 break     
	End 
	Return     
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[sp_helptext_GSM]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [sp_helptext_GSM]
(
@objName Varchar(Max),
@columnname Varchar(Max)=null,
@Qry Varchar(Max) Output
)
As
Begin
	Set NoCount On	
	Declare @dbname sysname,
	@objid int,
	@BlankSpaceAdded int, 
	@BasePos int,
	@CurrentPos int,
	@TextLength int,
	@LineId int,
	@AddOnLen int,
	@LFCR int,
	@DefinedLength int,
	@SyscomText nvarchar(4000),
	@Line nvarchar(255)

	Select @DefinedLength = 255
	Select @BlankSpaceAdded = 0 
	Create Table #CommentText
	(LineId	int, Text nvarchar(255) collate database_default)

	Select @dbname = parsename(@objname,3)
	If @dbname is null
		Select @dbname = db_name()
	else if @dbname <> db_name()
        begin
            raiserror(15250,-1,-1)
            return (1)
        end

	Select @objid = object_id(@objname)
	if (@objid is null)
        begin
			raiserror(15009,-1,-1,@objname,@dbname)
			return (1)
        end

	If (@columnname is not null)
		begin
			-- Check if it is a table
			if (Select count(*) from sys.objects where object_id = @objid and type in (''S '',''U '',''TF''))=0
				begin
					raiserror(15218,-1,-1,@objname)
					return(1)
				end
			-- check if it is a correct column name
			if ((Select ''count''=count(*) from sys.columns where name = @columnname and object_id = @objid) =0)
				begin
					raiserror(15645,-1,-1,@columnname)
					return(1)
				end
			if (ColumnProperty(@objid, @columnname, ''IsComputed'') = 0)
				begin
					raiserror(15646,-1,-1,@columnname)
					return(1)
				end
	        declare ms_crs_syscom  CURSOR LOCAL
			FOR Select text from syscomments where id = @objid and encrypted = 0 and number =
                (Select column_id from sys.columns where name = @columnname and object_id = @objid)
                order by number,colid
			FOR READ ONLY
		end
	else if @objid < 0	-- Handle system-objects
		begin
			-- Check count of rows with text data
			if (Select count(*) from master.sys.syscomments where id = @objid and text is not null) = 0
				begin
					raiserror(15197,-1,-1,@objname)
					return (1)
				end			
			declare ms_crs_syscom CURSOR LOCAL FOR Select text from master.sys.syscomments where id = @objid
			ORDER BY number, colid FOR READ ONLY
		end
	else
		begin
			if (Select count(*) from syscomments c, sysobjects o where o.xtype not in (''S'', ''U'') and o.id = c.id and o.id = @objid) = 0
                begin
                    raiserror(15197,-1,-1,@objname)
                    return (1)
                end
			if (Select count(*) from syscomments where id = @objid and encrypted = 0) = 0
                begin
                    raiserror(15471,-1,-1,@objname)
                    return (0)
                end
			declare ms_crs_syscom  CURSOR LOCAL
			FOR Select text from syscomments where id = @objid and encrypted = 0
				ORDER BY number, colid
			FOR READ ONLY
		end

	Select @LFCR = 2
	Select @LineId = 1
	OPEN ms_crs_syscom
	FETCH NEXT from ms_crs_syscom into @SyscomText
	WHILE @@fetch_status >= 0
	begin
		Select @BasePos = 1
		Select @CurrentPos = 1
		Select @TextLength = LEN(@SyscomText)

		WHILE @CurrentPos  != 0
		Begin
			--Looking for end of line followed by carriage return
			Select @CurrentPos =   CHARINDEX(char(13)+char(10), @SyscomText, @BasePos)
			--If carriage return found
			IF @CurrentPos != 0
				begin
					/*If new value for @Lines length will be > then the
					**set length then insert current contents of @line
					**and proceed.
					*/
					while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
					begin
						Select @AddOnLen = @DefinedLength-(isnull(LEN(@Line),0) + @BlankSpaceAdded)
						INSERT #CommentText VALUES
						( @LineId,
						  isnull(@Line, N'''') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N''''))
						Select @Line = NULL, @LineId = @LineId + 1,
							   @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
					end
					Select @Line    = isnull(@Line, N'''') + isnull(SUBSTRING(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'''')
					Select @BasePos = @CurrentPos+2
					INSERT #CommentText VALUES( @LineId, @Line )
					Select @LineId = @LineId + 1
					Select @Line = NULL
				end
			else
				--else carriage return not found
				begin
					IF @BasePos <= @TextLength
					begin
						/*If new value for @Lines length will be > then the
						**defined length
						*/
						while (isnull(LEN(@Line),0) + @BlankSpaceAdded + @TextLength-@BasePos+1 ) > @DefinedLength
						begin
							Select @AddOnLen = @DefinedLength - (isnull(LEN(@Line),0) + @BlankSpaceAdded)
							INSERT #CommentText VALUES
							( @LineId,
							  isnull(@Line, N'''') + isnull(SUBSTRING(@SyscomText, @BasePos, @AddOnLen), N''''))
							Select @Line = NULL, @LineId = @LineId + 1,
								@BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
						end
						Select @Line = isnull(@Line, N'''') + isnull(SUBSTRING(@SyscomText, @BasePos, @TextLength-@BasePos+1 ), N'''')
						if LEN(@Line) < @DefinedLength and charindex('' '', @SyscomText, @TextLength+1 ) > 0
						begin
							Select @Line = @Line + '' '', @BlankSpaceAdded = 1
						end
					end
				end
		end
		FETCH NEXT from ms_crs_syscom into @SyscomText
	end

	IF @Line is NOT NULL
		INSERT #CommentText VALUES( @LineId, @Line )
	
	CLOSE ms_crs_syscom
	DEALLOCATE ms_crs_syscom

	--Select * from #CommentText order by LineId

	Set @Qry = ''''
	Declare @Text As nvarchar(255)
	Declare CurCursor cursor for    
	Select Text from #CommentText order by LineId
	   
	open CurCursor    
	Fetch next From CurCursor Into @Text
	while @@Fetch_Status = 0    
	Begin    
		If @Qry = ''''
			Set @Qry = @Text
		Else
			Set @Qry = @Qry + @Text
		
		Fetch next From CurCursor Into @Text
	End    
	Close CurCursor    
	Deallocate CurCursor    

	DROP TABLE #CommentText
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SMSTemplateSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [SMSTemplateSettings](
	[SMSTemplateSettingsId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[SMSTemplateType] [varchar](max) NOT NULL,
	[SMSTemplate] [varchar](max) NOT NULL,
 CONSTRAINT [PK_SMSTemplateSettings] PRIMARY KEY CLUSTERED 
(
	[SMSTemplateSettingsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleVisitProperty]') AND type in (N'U'))
BEGIN
CREATE TABLE [ScheduleVisitProperty](
	[ScheduleVisitPropertyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ScheduleVisitId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_ScheduleVisitProperty] PRIMARY KEY CLUSTERED 
(
	[ScheduleVisitPropertyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleVisit]') AND type in (N'U'))
BEGIN
CREATE TABLE [ScheduleVisit](
	[ScheduleVisitId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Mobile] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[VisitDate] [datetime] NULL,
	[VisitTime] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_ScheduleVisit] PRIMARY KEY CLUSTERED 
(
	[ScheduleVisitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ScheduleTenantVisit]') AND type in (N'U'))
BEGIN
CREATE TABLE [ScheduleTenantVisit](
	[ScheduleTenantVisitId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[TenantId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[MaintenanceCategoryId] [int] NOT NULL,
	[VisitDate] [datetime] NULL,
	[VisitTime] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_ScheduleTenantVisit] PRIMARY KEY CLUSTERED 
(
	[ScheduleTenantVisitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SalesActivity]') AND type in (N'U'))
BEGIN
CREATE TABLE [SalesActivity](
	[SalesActivityId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[ActivityDate] [datetime] NULL,
	[ActivityTime] [varchar](max) NULL,
	[PropertyId] [bigint] NOT NULL,
	[ActivityTypeId] [bigint] NOT NULL,
	[IsDealComplete] [bigint] NOT NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[BriefRemarks] [varchar](max) NOT NULL CONSTRAINT [DF_SalesActivity_BriefRemarks]  DEFAULT (''),
 CONSTRAINT [PK_SalesActivity] PRIMARY KEY CLUSTERED 
(
	[SalesActivityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SageDB]') AND type in (N'U'))
BEGIN
CREATE TABLE [SageDB](
	[SageDBId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[ServerName] [varchar](max) NOT NULL,
	[AuthType] [int] NOT NULL,
	[DatabaseName] [varchar](max) NOT NULL,
	[UserName] [varchar](max) NOT NULL,
	[UserPassword] [varchar](max) NOT NULL,
	[BatchesId] [int] NOT NULL CONSTRAINT [DF_SageDB_BatchesId]  DEFAULT ((0)),
 CONSTRAINT [PK_SageDB] PRIMARY KEY CLUSTERED 
(
	[SageDBId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RequestMaintenanceCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [RequestMaintenanceCategory](
	[RequestMaintenanceCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[RequestInspectionId] [int] NULL,
	[TenantId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[MaintenanceCategoryDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_RequestMaintenanceCategory] PRIMARY KEY CLUSTERED 
(
	[RequestMaintenanceCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RequestInspection]') AND type in (N'U'))
BEGIN
CREATE TABLE [RequestInspection](
	[RequestInspectionId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[TenantId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[InspectionDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_RequestInspection] PRIMARY KEY CLUSTERED 
(
	[RequestInspectionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[RecordNoSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [RecordNoSettings](
	[CompanyId] [int] NOT NULL,
	[Id] [bigint] NULL,
	[Prefix] [varchar](max) NULL,
	[StartingNo] [int] NULL,
	[CLength] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyType]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyType](
	[PropertyTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyTypeName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED 
(
	[PropertyTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyLocationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyLocationType](
	[PropertyLocationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[LocationTypeId] [int] NOT NULL,
	[LocationValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PropertyLocationType] PRIMARY KEY CLUSTERED 
(
	[PropertyLocationTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyFeature]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyFeature](
	[PropertyFeatureId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[FeatureId] [int] NOT NULL,
	[FeatureValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PropertyFeature] PRIMARY KEY CLUSTERED 
(
	[PropertyFeatureId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyColumns]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyColumns](
	[PropertyColumnsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ColumnList] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PropertyColumns] PRIMARY KEY CLUSTERED 
(
	[PropertyColumnsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyCharge]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyCharge](
	[PropertyChargeId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[ChargeId] [int] NOT NULL,
	[Mode] [nvarchar](max) NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_PropertyCharge] PRIMARY KEY CLUSTERED 
(
	[PropertyChargeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyBuildingParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyBuildingParameter](
	[PropertyBuildingParameterId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[BuildingParameterId] [int] NOT NULL,
	[BuildingParameterValue] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PropertyBuildingParameter] PRIMARY KEY CLUSTERED 
(
	[PropertyBuildingParameterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PropertyAmenity]') AND type in (N'U'))
BEGIN
CREATE TABLE [PropertyAmenity](
	[PropertyAmenityId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[AmenityId] [int] NOT NULL,
 CONSTRAINT [PK_PropertyAmenity] PRIMARY KEY CLUSTERED 
(
	[PropertyAmenityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Property]') AND type in (N'U'))
BEGIN
CREATE TABLE [Property](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[PostedBy] [smallint] NOT NULL,
	[ProfileId] [int] NOT NULL,
	[Type] [smallint] NOT NULL,
	[PropertyLrNo] [varchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[Status] [smallint] NULL DEFAULT ((0)),
	[PropertyFor] [int] NOT NULL CONSTRAINT [DF_Property_PropertyFor]  DEFAULT ((0)),
	[PropertyValue] [numeric](18, 2) NOT NULL CONSTRAINT [DF_Property_PropertyValue]  DEFAULT ((0)),
	[SalesPersonId] [bigint] NOT NULL CONSTRAINT [DF_Property_SalesPersonId]  DEFAULT ((0)),
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[prcPropertyInformationGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [prcPropertyInformationGet]
	@ViewName Varchar(Max) = '''',  
	@FieldName Varchar(Max) = '''',
	@CompanyId BigInt = 0
As
Begin
	Declare @sQry As Varchar(Max) = ''''
	If Isnull(@FieldName, '''') = ''''
		Begin  
			Set @sQry = ''Select Qry.* From 			
			(Select dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.*,   
	Isnull(dbo.Property.Type, 1) As Type, Isnull(dbo.PropertyType.PropertyTypeName, ''''Residential'''') As [Property Type], 
	dbo.Property.PostedBy, Case dbo.Property.PostedBy WHEN 1 THEN ''''Landlord'''' WHEN 2 THEN ''''Broker'''' WHEN 3 THEN ''''Employee'''' END As [Posted By], dbo.Property.ProfileId, 
	Case dbo.Property.PostedBy WHEN 1 THEN dbo.vwLandlordGet.[Landlord Name] WHEN 2 THEN dbo.vwBrokerGet.[Broker Name] WHEN 3 THEN dbo.vwEmployeeGet.[Employee Name] END AS [Posted By Name], 
	dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.CreatedOn, dbo.Property.ModifiedOn, dbo.Property.DeletedOn, 
	dbo.Property.Status, CASE dbo.Property.Status WHEN 1 THEN ''''No'''' WHEN 0 THEN ''''Yes'''' END AS Available, 
	dbo.Property.PropertyFor, Case dbo.Property.PropertyFor When 1 Then ''''For Rent'''' When 2 Then ''''For Sale'''' Else '''''''' End As [Property For], 
	dbo.Property.PropertyValue As [Property Value]
From dbo.Property 
	Left Outer Join dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' On dbo.Property.PropertyId = dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.PropertyId  
	Left Outer Join dbo.vwLandlordGet ON dbo.Property.ProfileId = dbo.vwLandlordGet.LandlordId 
	Left Outer Join dbo.vwBrokerGet ON dbo.Property.ProfileId = dbo.vwBrokerGet.BrokerId
	Left Outer Join dbo.vwEmployeeGet ON dbo.Property.ProfileId = dbo.vwEmployeeGet.EmployeeId
	Left Outer Join dbo.PropertyType ON dbo.Property.Type = dbo.PropertyType.PropertyTypeId
Where dbo.Property.DeletedOn Is Null) Qry '' + @ViewName
			Exec (@sQry)			
		End  
	Else  
		Begin  
			Set @sQry = ''Select '' + @FieldName + '' From 			
			(Select dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.*,   
	Isnull(dbo.Property.Type, 1) As Type, Isnull(dbo.PropertyType.PropertyTypeName, ''''Residential'''') As [Property Type], 
	dbo.Property.PostedBy, Case dbo.Property.PostedBy WHEN 1 THEN ''''Landlord'''' WHEN 2 THEN ''''Broker'''' WHEN 3 THEN ''''Employee'''' END As [Posted By], dbo.Property.ProfileId, 
	Case dbo.Property.PostedBy WHEN 1 THEN dbo.vwLandlordGet.[Landlord Name] WHEN 2 THEN dbo.vwBrokerGet.[Broker Name] WHEN 3 THEN dbo.vwEmployeeGet.[Employee Name] END AS [Posted By Name], 
	dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.CreatedOn, dbo.Property.ModifiedOn, dbo.Property.DeletedOn, 
	dbo.Property.Status, CASE dbo.Property.Status WHEN 1 THEN ''''No'''' WHEN 0 THEN ''''Yes'''' END AS Available, 
	dbo.Property.PropertyFor, Case dbo.Property.PropertyFor When 1 Then ''''For Rent'''' When 2 Then ''''For Sale'''' Else '''''''' End As [Property For], 
	dbo.Property.PropertyValue As [Property Value]
From dbo.Property 
	Left Outer Join dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' On dbo.Property.PropertyId = dbo.PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.PropertyId  
	Left Outer Join dbo.vwLandlordGet ON dbo.Property.ProfileId = dbo.vwLandlordGet.LandlordId 
	Left Outer Join dbo.vwBrokerGet ON dbo.Property.ProfileId = dbo.vwBrokerGet.BrokerId
	Left Outer Join dbo.vwEmployeeGet ON dbo.Property.ProfileId = dbo.vwEmployeeGet.EmployeeId
	Left Outer Join dbo.PropertyType ON dbo.Property.Type = dbo.PropertyType.PropertyTypeId
Where dbo.Property.DeletedOn Is Null) Qry '' + @ViewName
			Exec (@sQry)
		End  
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[prcObjectListGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [prcObjectListGet]
	@TableList Varchar(Max)
As
Begin
	Create Table #TableList(TableName Varchar(Max))
	Insert Into #TableList Select items from dbo.Split(@TableList, '','')
	

	Create Table #FoundTableList(TableName Varchar(Max), ObjectName Varchar(Max), ObjectType Varchar(Max))


	Declare @Name As Varchar(Max), @xType As Varchar(Max), @TableName As Varchar(Max), @Qry Varchar(Max)
	Declare CurProcedure cursor for    
	Select Name, xType From Sysobjects Where xtype In (''P'', ''TR'', ''FN'', ''V'', ''TF'') Order By Name Asc
	   
	open CurProcedure    
	Fetch next From CurProcedure Into @Name, @xType
	while @@Fetch_Status = 0    
	Begin    
		Set @Qry = ''''
		Exec dbo.sp_helptext_GSM @Name, null, @Qry Output

		Declare CurTableList cursor for    
		Select TableName From #TableList
		   
		open CurTableList    
		Fetch next From CurTableList Into @TableName
		while @@Fetch_Status = 0    
		Begin    
			
			If CHARINDEX(@TableName,@Qry,0) > 0
				Insert Into #FoundTableList Select @TableName, @Name, @xType
				
			Fetch next From CurTableList Into @TableName
		End    
		Close CurTableList    
		Deallocate CurTableList    


			
		Fetch next From CurProcedure Into @Name, @xType
	End    
	Close CurProcedure    
	Deallocate CurProcedure    


	Select Distinct ObjectName, ObjectType From #FoundTableList

	Drop Table #FoundTableList
	Drop Table #TableList
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Feature_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Feature_Select]
	@Op As nvarchar(Max) = '''',
	@FeatureName As nvarchar(Max) = '''',
	@FeatureId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select FeatureId From Feature Where FeatureName = @FeatureName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select FeatureId From Feature Where FeatureName = @FeatureName And FeatureId <> @FeatureId And CompanyId = @CompanyId	
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select FeatureId As [Value], Feature As [Text] From vwFeatureGet Where '' +  @Search + '' Order By Feature Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select FeatureId, Feature, Description From vwFeatureGet Where '' +  @Search + '' Order By Feature Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BuildingParameter_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_BuildingParameter_Select]
	@Op As nvarchar(Max) = '''',
	@BuildingParameterName As nvarchar(Max) = '''',
	@BuildingParameterId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select BuildingParameterId From BuildingParameter Where BuildingParameterName = @BuildingParameterName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select BuildingParameterId From BuildingParameter Where BuildingParameterName = @BuildingParameterName And BuildingParameterId <> @BuildingParameterId And CompanyId = @CompanyId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select BuildingParameterId As [Value], [Building Parameter] As [Text] From vwBuildingParameterGet Where '' +  @Search + '' Order By [Building Parameter] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select BuildingParameterId, [Building Parameter], Description From vwBuildingParameterGet Where '' +  @Search + '' Order By [Building Parameter] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Charge_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Charge_Select]
	@Op As nvarchar(Max) = '''',
	@ChargeName As nvarchar(Max) = '''',
	@ChargeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select ChargeId From Charge Where ChargeName = @ChargeName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select ChargeId From Charge Where ChargeName = @ChargeName And ChargeId <> @ChargeId And CompanyId = @CompanyId	
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select ChargeId As [Value], Charge As [Text] From vwChargeGet Where '' +  @Search + '' Order By Charge Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select ChargeId, Charge, Description From vwChargeGet Where '' +  @Search + '' Order By Charge Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_City_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_City_Select]
	@Op As nvarchar(Max) = '''',
	@CityName As nvarchar(Max) = '''',
	@CityId As BigInt = 0,
	@StateId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select CityId From City Where CityName = @CityName And StateId = @StateId And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select CityId From City Where CityName = @CityName And CityId <> @CityId And StateId = @StateId And CompanyId = @CompanyId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select CityId As [Value], City As [Text] From vwCityGet Where '' +  @Search + '' Order By City Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select CityId,StateId,State,CountryId,Country,City,Description From vwCityGet Where '' +  @Search + '' Order By City Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_CompanyDetailType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_CompanyDetailType_Select]
	@Op As nvarchar(Max) = '''',
	@CompanyDetailTypeName As nvarchar(Max) = '''',
	@CompanyDetailTypeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select CompanyDetailTypeId From CompanyDetailType Where CompanyDetailTypeName = @CompanyDetailTypeName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select CompanyDetailTypeId From CompanyDetailType Where CompanyDetailTypeName = @CompanyDetailTypeName And CompanyDetailTypeId <> @CompanyDetailTypeId And CompanyId = @CompanyId	
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select CompanyDetailTypeId As [Value], [Company Detail Type] As [Text] From vwCompanyDetailTypeGet Where '' +  @Search + '' Order By [Company Detail Type] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select CompanyDetailTypeId, [Company Detail Type], TextMode, Description, [Detail Type] From vwCompanyDetailTypeGet Where '' +  @Search + '' Order By [Company Detail Type] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Country_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Country_Select]
	@Op As nvarchar(Max) = '''',
	@CountryName As nvarchar(Max) = '''',
	@CountryId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select CountryId From Country Where CountryName = @CountryName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select CountryId From Country Where CountryName = @CountryName And CountryId <> @CountryId And CompanyId = @CompanyId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select CountryId As [Value], Country As [Text] From vwCountryGet Where '' +  @Search + '' Order By Country Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select CountryId, Country, Description From vwCountryGet Where '' +  @Search + '' Order By Country Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_PersonalDetailType_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_PersonalDetailType_Upd] ON 
	[PersonalDetailType] After Update
As 
Begin
    Set NoCount On
    Declare @PersonalDetailTypeName As Varchar(Max) = ''''
    Declare @OLDPersonalDetailTypeName As Varchar(Max) = ''''
    Declare @OLDPersonalDetailType_DftConstraint As Varchar(Max) = ''''
    
    Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From Inserted    
    Select @OLDPersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From Deleted
    
    Set @OLDPersonalDetailType_DftConstraint = ''''
    Select @OLDPersonalDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''BrokerInformation'' And Name = @OLDPersonalDetailTypeName
    If Isnull(@OLDPersonalDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table BrokerInformation Drop Constraint '' + @OLDPersonalDetailType_DftConstraint)
    End	

	Set @OLDPersonalDetailType_DftConstraint = ''''
    Select @OLDPersonalDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''LawyerInformation'' And Name = @OLDPersonalDetailTypeName
    If Isnull(@OLDPersonalDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table LawyerInformation Drop Constraint '' + @OLDPersonalDetailType_DftConstraint)
    End	

    Set @OLDPersonalDetailType_DftConstraint = ''''
    Select @OLDPersonalDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''EmployeeInformation'' And Name = @OLDPersonalDetailTypeName
    If Isnull(@OLDPersonalDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table EmployeeInformation Drop Constraint '' + @OLDPersonalDetailType_DftConstraint)
    End	

    Set @OLDPersonalDetailType_DftConstraint = ''''
    Select @OLDPersonalDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''LandlordInformation'' And Name = @OLDPersonalDetailTypeName
    If Isnull(@OLDPersonalDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table LandlordInformation Drop Constraint '' + @OLDPersonalDetailType_DftConstraint)
    End	

    Set @OLDPersonalDetailType_DftConstraint = ''''
    Select @OLDPersonalDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''TenantInformation'' And Name = @OLDPersonalDetailTypeName
    If Isnull(@OLDPersonalDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table TenantInformation Drop Constraint '' + @OLDPersonalDetailType_DftConstraint)
    End	

    Declare @str As Varchar(Max) = ''''
	Set @str = ''sp_RENAME ''''BrokerInformation.[''+ @OLDPersonalDetailTypeName +'']'''' , '''''' + @PersonalDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table BrokerInformation Add Constraint DF_BrokerInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @PersonalDetailTypeName + '']''
	Exec (@str)

	Set @str = ''sp_RENAME ''''LawyerInformation.[''+ @OLDPersonalDetailTypeName +'']'''' , '''''' + @PersonalDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table LawyerInformation Add Constraint DF_LawyerInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @PersonalDetailTypeName + '']''
	Exec (@str)
    
	Set @str = ''sp_RENAME ''''EmployeeInformation.[''+ @OLDPersonalDetailTypeName +'']'''' , '''''' + @PersonalDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table EmployeeInformation Add Constraint DF_EmployeeInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @PersonalDetailTypeName + '']''
	Exec (@str)
    
	Set @str = ''sp_RENAME ''''LandlordInformation.[''+ @OLDPersonalDetailTypeName +'']'''' , '''''' + @PersonalDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table LandlordInformation Add Constraint DF_LandlordInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @PersonalDetailTypeName + '']''
	Exec (@str)

	Set @str = ''sp_RENAME ''''TenantInformation.[''+ @OLDPersonalDetailTypeName +'']'''' , '''''' + @PersonalDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table TenantInformation Add Constraint DF_TenantInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @PersonalDetailTypeName + '']''
	Exec (@str)

End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_PersonalDetailType_Ins]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_PersonalDetailType_Ins] ON 
	[PersonalDetailType] After Insert
As 
Begin
    Set NoCount On
    Declare @PersonalDetailTypeName As Varchar(Max) = ''''
    Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @PersonalDetailTypeName And Object_Name(Object_Id) = ''BrokerInformation'')
		Begin
			Set @str = ''Alter Table BrokerInformation Add [''+ @PersonalDetailTypeName +''] Varchar(Max) Not Null Constraint DF_BrokerInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @PersonalDetailTypeName And Object_Name(Object_Id) = ''LawyerInformation'')
		Begin
			Set @str = ''Alter Table LawyerInformation Add [''+ @PersonalDetailTypeName +''] Varchar(Max) Not Null Constraint DF_LawyerInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @PersonalDetailTypeName And Object_Name(Object_Id) = ''EmployeeInformation'')
		Begin
			Set @str = ''Alter Table EmployeeInformation Add [''+ @PersonalDetailTypeName +''] Varchar(Max) Not Null Constraint DF_EmployeeInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @PersonalDetailTypeName And Object_Name(Object_Id) = ''LandlordInformation'')
		Begin
			Set @str = ''Alter Table LandlordInformation Add [''+ @PersonalDetailTypeName +''] Varchar(Max) Not Null Constraint DF_LandlordInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @PersonalDetailTypeName And Object_Name(Object_Id) = ''TenantInformation'')
		Begin
			Set @str = ''Alter Table TenantInformation Add [''+ @PersonalDetailTypeName +''] Varchar(Max) Not Null Constraint DF_TenantInformation_'' + Replace(@PersonalDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_LocationType_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_LocationType_Upd] ON 
	[LocationType] After Update
As 
Begin
    Set NoCount On
    Declare @LocationTypeName As Varchar(Max) = ''''
    Declare @OLDLocationTypeName As Varchar(Max) = ''''
    Declare @OLDLocationType_DftConstraint As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    
    Select @LocationTypeName = Isnull(LocationTypeName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted    
    Select @OLDLocationTypeName = Isnull(LocationTypeName, '''') From Deleted
    
    Declare @str As Varchar(Max) = ''''
    Set @OLDLocationType_DftConstraint = ''''
    Declare @ObjectName As Varchar(Max) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId)
    Select @OLDLocationType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = @ObjectName And Name = @OLDLocationTypeName
    If Isnull(@OLDLocationType_DftConstraint, '''') <> ''''
    Begin
		Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Drop Constraint '' + @OLDLocationType_DftConstraint
        Exec (@str)
    End	

	Set @str = ''sp_RENAME ''''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.[''+ @OLDLocationTypeName +'']'''' , '''''' + @LocationTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@LocationTypeName, '' '', '''') + '' Default '''''''' For ['' + @LocationTypeName + '']''
	Exec (@str)

End'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_LocationType_Ins]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_LocationType_Ins] ON 
	[LocationType] After Insert
As 
Begin
    Set NoCount On
    Declare @LocationTypeName As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    Select @LocationTypeName = Isnull(LocationTypeName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @LocationTypeName And Object_Name(Object_Id) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId))
		Begin
			Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add [''+ @LocationTypeName +''] Varchar(Max) Not Null Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@LocationTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_Feature_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_Feature_Upd] ON 
	[Feature] After Update
As 
Begin
    Set NoCount On
    Declare @FeatureName As Varchar(Max) = ''''
    Declare @OLDFeatureName As Varchar(Max) = ''''
    Declare @OLDFeature_DftConstraint As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    
    Select @FeatureName = Isnull(FeatureName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted    
    Select @OLDFeatureName = Isnull(FeatureName, '''') From Deleted
    
    Declare @str As Varchar(Max) = ''''
	Set @OLDFeature_DftConstraint = ''''
	Declare @ObjectName As Varchar(Max) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId)
    Select @OLDFeature_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = @ObjectName And Name = @OLDFeatureName
    If Isnull(@OLDFeature_DftConstraint, '''') <> ''''
    Begin
		Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Drop Constraint '' + @OLDFeature_DftConstraint
        Exec (@str)
    End	

    Set @str = ''sp_RENAME ''''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.[''+ @OLDFeatureName +'']'''' , '''''' + @FeatureName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@FeatureName, '' '', '''') + '' Default '''''''' For ['' + @FeatureName + '']''
	Exec (@str)

End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_Feature_Ins]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_Feature_Ins] ON 
	[Feature] After Insert
As 
Begin
    Set NoCount On
    Declare @FeatureName As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    Select @FeatureName = Isnull(FeatureName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @FeatureName And Object_Name(Object_Id) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId))
		Begin
			Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add [''+ @FeatureName +''] Varchar(Max) Not Null Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@FeatureName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_CompanyDetailType_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_CompanyDetailType_Upd] ON 
	[CompanyDetailType] After Update
As 
Begin
    Set NoCount On
    Declare @CompanyDetailTypeName As Varchar(Max) = ''''
    Declare @OLDCompanyDetailTypeName As Varchar(Max) = ''''
    Declare @OLDCompanyDetailType_DftConstraint As Varchar(Max) = ''''
    
    Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From Inserted    
    Select @OLDCompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From Deleted
    
    Set @OLDCompanyDetailType_DftConstraint = ''''
    Select @OLDCompanyDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''BrokerCompanyInformation'' And Name = @OLDCompanyDetailTypeName
    If Isnull(@OLDCompanyDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table BrokerCompanyInformation Drop Constraint '' + @OLDCompanyDetailType_DftConstraint)
    End	

    Set @OLDCompanyDetailType_DftConstraint = ''''
    Select @OLDCompanyDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''LawyerCompanyInformation'' And Name = @OLDCompanyDetailTypeName
    If Isnull(@OLDCompanyDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table LawyerCompanyInformation Drop Constraint '' + @OLDCompanyDetailType_DftConstraint)
    End	

    Set @OLDCompanyDetailType_DftConstraint = ''''
    Select @OLDCompanyDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''EmployeeCompanyInformation'' And Name = @OLDCompanyDetailTypeName
    If Isnull(@OLDCompanyDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table EmployeeCompanyInformation Drop Constraint '' + @OLDCompanyDetailType_DftConstraint)
    End	

    Set @OLDCompanyDetailType_DftConstraint = ''''
    Select @OLDCompanyDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''LandlordCompanyInformation'' And Name = @OLDCompanyDetailTypeName
    If Isnull(@OLDCompanyDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table LandlordCompanyInformation Drop Constraint '' + @OLDCompanyDetailType_DftConstraint)
    End	

    Set @OLDCompanyDetailType_DftConstraint = ''''
    Select @OLDCompanyDetailType_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = ''TenantCompanyInformation'' And Name = @OLDCompanyDetailTypeName
    If Isnull(@OLDCompanyDetailType_DftConstraint, '''') <> ''''
    Begin
        Exec (''Alter Table TenantCompanyInformation Drop Constraint '' + @OLDCompanyDetailType_DftConstraint)
    End	
        
    Declare @str As Varchar(Max) = ''''
	Set @str = ''sp_RENAME ''''BrokerCompanyInformation.[''+ @OLDCompanyDetailTypeName +'']'''' , '''''' + @CompanyDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table BrokerCompanyInformation Add Constraint DF_BrokerCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @CompanyDetailTypeName + '']''
	Exec (@str)

	Set @str = ''sp_RENAME ''''LawyerCompanyInformation.[''+ @OLDCompanyDetailTypeName +'']'''' , '''''' + @CompanyDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table LawyerCompanyInformation Add Constraint DF_LawyerCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @CompanyDetailTypeName + '']''
	Exec (@str)

	Set @str = ''sp_RENAME ''''EmployeeCompanyInformation.[''+ @OLDCompanyDetailTypeName +'']'''' , '''''' + @CompanyDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table EmployeeCompanyInformation Add Constraint DF_EmployeeCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @CompanyDetailTypeName + '']''
	Exec (@str)
	
	Set @str = ''sp_RENAME ''''LandlordCompanyInformation.[''+ @OLDCompanyDetailTypeName +'']'''' , '''''' + @CompanyDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table LandlordCompanyInformation Add Constraint DF_LandlordCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @CompanyDetailTypeName + '']''
	Exec (@str)

	Set @str = ''sp_RENAME ''''TenantCompanyInformation.[''+ @OLDCompanyDetailTypeName +'']'''' , '''''' + @CompanyDetailTypeName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table TenantCompanyInformation Add Constraint DF_TenantCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default '''''''' For ['' + @CompanyDetailTypeName + '']''
	Exec (@str)

End'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_CompanyDetailType_Ins]'))
EXEC dbo.sp_executesql @statement = N'CREATE Trigger [trg_CompanyDetailType_Ins] ON 
	[CompanyDetailType] After Insert
As 
Begin
    Set NoCount On
    Declare @CompanyDetailTypeName As Varchar(Max) = ''''
    Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @CompanyDetailTypeName And Object_Name(Object_Id) = ''BrokerCompanyInformation'')
		Begin
			Set @str = ''Alter Table BrokerCompanyInformation Add [''+ @CompanyDetailTypeName +''] Varchar(Max) Not Null Constraint DF_BrokerCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @CompanyDetailTypeName And Object_Name(Object_Id) = ''LawyerCompanyInformation'')
		Begin
			Set @str = ''Alter Table LawyerCompanyInformation Add [''+ @CompanyDetailTypeName +''] Varchar(Max) Not Null Constraint DF_LawyerCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @CompanyDetailTypeName And Object_Name(Object_Id) = ''EmployeeCompanyInformation'')
		Begin
			Set @str = ''Alter Table EmployeeCompanyInformation Add [''+ @CompanyDetailTypeName +''] Varchar(Max) Not Null Constraint DF_EmployeeCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @CompanyDetailTypeName And Object_Name(Object_Id) = ''LandlordCompanyInformation'')
		Begin
			Set @str = ''Alter Table LandlordCompanyInformation Add [''+ @CompanyDetailTypeName +''] Varchar(Max) Not Null Constraint DF_LandlordCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End

    If Not Exists (Select 1 From sys.Columns Where Name = @CompanyDetailTypeName And Object_Name(Object_Id) = ''TenantCompanyInformation'')
		Begin
			Set @str = ''Alter Table TenantCompanyInformation Add [''+ @CompanyDetailTypeName +''] Varchar(Max) Not Null Constraint DF_TenantCompanyInformation_'' + Replace(@CompanyDetailTypeName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_BuildingParameter_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_BuildingParameter_Upd] ON 
	[BuildingParameter] After Update
As 
Begin
    Set NoCount On
    Declare @BuildingParameterName As Varchar(Max) = ''''
    Declare @OLDBuildingParameterName As Varchar(Max) = ''''
    Declare @OLDBuildingParameter_DftConstraint As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    
    Select @BuildingParameterName = Isnull(BuildingParameterName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted    
    Select @OLDBuildingParameterName = Isnull(BuildingParameterName, '''') From Deleted
    
    Declare @str As Varchar(Max) = ''''
	Set @OLDBuildingParameter_DftConstraint = ''''
	Declare @ObjectName As Varchar(Max) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId)
    Select @OLDBuildingParameter_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = @ObjectName And Name = @OLDBuildingParameterName
    If Isnull(@OLDBuildingParameter_DftConstraint, '''') <> ''''
    Begin
		Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Drop Constraint '' + @OLDBuildingParameter_DftConstraint
        Exec (@str)
    End	

    Set @str = ''sp_RENAME ''''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.[''+ @OLDBuildingParameterName +'']'''' , '''''' + @BuildingParameterName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@BuildingParameterName, '' '', '''') + '' Default '''''''' For ['' + @BuildingParameterName + '']''
	Exec (@str)

End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_BuildingParameter_Ins]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_BuildingParameter_Ins] ON 
	[BuildingParameter] After Insert
As 
Begin
    Set NoCount On
    Declare @BuildingParameterName As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    Select @BuildingParameterName = Isnull(BuildingParameterName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @BuildingParameterName And Object_Name(Object_Id) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId))
		Begin
			Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add [''+ @BuildingParameterName +''] Varchar(Max) Not Null Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@BuildingParameterName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_Amenity_Upd]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_Amenity_Upd] ON 
	[Amenity] After Update
As 
Begin
    Set NoCount On
    Declare @AmenityName As Varchar(Max) = ''''
    Declare @OLDAmenityName As Varchar(Max) = ''''
    Declare @OLDAmenity_DftConstraint As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    
    Select @AmenityName = Isnull(AmenityName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted    
    Select @OLDAmenityName = Isnull(AmenityName, '''') From Deleted
    
    Declare @str As Varchar(Max) = ''''
	Set @OLDAmenity_DftConstraint = ''''
	Declare @ObjectName As Varchar(Max) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId)
    Select @OLDAmenity_DftConstraint = Isnull(Object_Name(Default_Object_Id), '''') From Sys.Columns Where Object_Name(Object_Id) = @ObjectName And Name = @OLDAmenityName
    If Isnull(@OLDAmenity_DftConstraint, '''') <> ''''
    Begin
		Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Drop Constraint '' + @OLDAmenity_DftConstraint
        Exec (@str)
    End	

    Set @str = ''sp_RENAME ''''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''.[''+ @OLDAmenityName +'']'''' , '''''' + @AmenityName + '''''', ''''COLUMN''''''
	Exec (@str)
	Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@AmenityName, '' '', '''') + '' Default '''''''' For ['' + @AmenityName + '']''
	Exec (@str)

End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[trg_Amenity_Ins]'))
EXEC dbo.sp_executesql @statement = N'
CREATE Trigger [trg_Amenity_Ins] ON 
	[Amenity] After Insert
As 
Begin
    Set NoCount On
    Declare @AmenityName As Varchar(Max) = ''''
    Declare @CompanyId As BigInt = 0
    Select @AmenityName = Isnull(AmenityName, ''''), @CompanyId = Isnull(CompanyId, 0) From Inserted
    
    Declare @str As Varchar(Max) = ''''
    If Not Exists (Select 1 From sys.Columns Where Name = @AmenityName And Object_Name(Object_Id) = ''PropertyInformation_'' + Convert(Varchar(Max), @CompanyId))
		Begin
			Set @str = ''Alter Table PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Add [''+ @AmenityName +''] Varchar(Max) Not Null Constraint DF_PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + ''_'' + Replace(@AmenityName, '' '', '''') + '' Default ''''''''''
			Exec (@str)
		End
End

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeaseSignUp](
	[LeaseSignUpId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpNo] [varchar](max) NOT NULL,
	[LeaseSignUpDate] [datetime] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[DueDate] [int] NOT NULL,
	[BrokerId] [int] NULL,
	[BrokerValue] [varchar](max) NULL,
	[LawyerId] [int] NULL,
	[LawyerValue] [varchar](max) NULL,
	[SendFeeNote] [int] NOT NULL,
	[SendFeeNote_EmailId] [varchar](max) NOT NULL,
	[SendRentRemindEmail] [int] NOT NULL,
	[SendRentRemindEmail_EmailId] [varchar](max) NOT NULL,
	[SendRentRemindSMS] [int] NOT NULL,
	[SendRentRemindSMS_ContactNo] [varchar](max) NOT NULL,
	[ReminderCount] [int] NOT NULL,
	[ReminderDueDays] [int] NOT NULL,
	[SendRentDelayEmail] [int] NOT NULL,
	[SendRentDelayEmail_EmailId] [varchar](max) NOT NULL,
	[SendRentDelaySMS] [int] NOT NULL,
	[SendRentDelaySMS_ContactNo] [varchar](max) NOT NULL,
	[DelayCount] [int] NOT NULL,
	[DelayDueDays] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[TerminateFlag] [smallint] NOT NULL CONSTRAINT [DF__LeaseSign__Termi__17F790F9]  DEFAULT ((0)),
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF__LeaseSign__Creat__18EBB532]  DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_LeaseSignUp] PRIMARY KEY CLUSTERED 
(
	[LeaseSignUpId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ActivityType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_ActivityType_Select]
	@Op As nvarchar(Max) = '''',
	@ActivityTypeName As nvarchar(Max) = '''',
	@ActivityTypeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select ActivityTypeId From ActivityType Where ActivityTypeName = @ActivityTypeName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select ActivityTypeId From ActivityType Where ActivityTypeName = @ActivityTypeName And ActivityTypeId <> @ActivityTypeId And CompanyId = @CompanyId
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select ActivityTypeId As [Value], [Activity Type] As [Text] From vwActivityTypeGet Where '' +  @Search + '' Order By [Activity Type] Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select ActivityTypeId, [Activity Type], Description From vwActivityTypeGet Where '' +  @Search + '' Order By [Activity Type] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Amenity_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Amenity_Select]
	@Op As nvarchar(Max) = '''',
	@AmenityName As nvarchar(Max) = '''',
	@AmenityType As smallint = 0,
	@AmenityId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select AmenityId From Amenity Where AmenityName = @AmenityName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select AmenityId From Amenity Where AmenityName = @AmenityName And AmenityId <> @AmenityId And CompanyId = @CompanyId	
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select AmenityId As [Value], Amenity As [Text] From vwAmenityGet Where '' +  @Search + '' Order By Amenity Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select AmenityId, Amenity, [Amenity Type], Description From vwAmenityGet Where '' +  @Search + '' Order By Amenity Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Area_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Area_Select]
	@Op As nvarchar(Max) = '''',
	@AreaName As nvarchar(Max) = '''',
	@AreaId As BigInt = 0,
	@ZoneId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select AreaId From Area Where AreaName = @AreaName And ZoneId = @ZoneId And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select AreaId From Area Where AreaName = @AreaName And AreaId <> @AreaId And ZoneId = @ZoneId And CompanyId = @CompanyId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select AreaId As [Value], Area As [Text] From vwAreaGet Where '' +  @Search + '' Order By Area Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select AreaId,ZoneId,Zone,CityId,City,StateId,State,CountryId,Country,Area,Description From vwAreaGet Where '' +  @Search + '' Order By Area Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[IssueAllocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [IssueAllocation](
	[IssueAllocateId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[IssueRecordId] [int] NULL,
	[AssignTo] [int] NULL,
	[AssignToId] [int] NULL,
	[ResolvedByDate] [datetime] NOT NULL,
	[IsChargeble] [int] NULL,
	[Amount] [varchar](max) NULL,
	[EmailTo] [int] NULL,
	[EmailToId] [int] NULL,
	[Description] [varchar](max) NULL,
	[StatusId] [int] NULL,
	[ProgressDate] [datetime] NULL,
	[ExpCompDate] [datetime] NULL,
	[IsCompleted] [int] NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_IssueAllocation_CreatedOn]  DEFAULT (getdate()),
	[ModifiedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_IssueAllocation] PRIMARY KEY CLUSTERED 
(
	[IssueAllocateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SageDB]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_SageDB]
	@SageDBId As bigint = 0,
	@CompanyId As bigint = 0,
	@ServerName As Varchar(Max) = '''',
	@AuthType As Int = 0,
	@UserName As Varchar(Max) = '''', 
	@UserPassword As Varchar(Max) = '''',
	@DatabaseName As Varchar(Max) = '''',
	@BatchesId As Int = 0,
	@Op As Varchar(Max),
	@ReturnValue As BigInt = 0 Output 
As
Begin
	If @Op = ''I''
		Begin
			Insert Into SageDB (CompanyId, ServerName, AuthType, UserName, UserPassword, DatabaseName, BatchesId)
			Values (@CompanyId, @ServerName, @AuthType, @UserName, @UserPassword, @DatabaseName, @BatchesId)
			
			Set @ReturnValue = Scope_Identity()			
		End
	Else If @Op = ''U''
		Begin
			Update SageDB Set 
				ServerName = @ServerName, AuthType = @AuthType, UserName = @UserName, UserPassword = @UserPassword, 
				DatabaseName = @DatabaseName, BatchesId = @BatchesId
			Where SageDBId = @SageDBId
			Set @ReturnValue = @SageDBId
		End
	Else If @Op = ''D''
		Begin
			Delete From SageDB Where SageDBId = @SageDBId
			Set @ReturnValue = @SageDBId
		End
	Else If @Op = ''S''
		Begin
			Select SageDBId, ServerName As [Server Name], AuthType, DatabaseName As [Database Name], UserName As [User Name], UserPassword As [User Password], BatchesId
			From SageDB Where CompanyId = @CompanyId
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_TenantGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwLeaseSignUp_TenantGet] As 
Select dbo.LeaseSignUp_Tenant.LeaseSignUp_TenantId, dbo.LeaseSignUp_Tenant.LeaseSignUpId, dbo.LeaseSignUp_Tenant.TenantId, 
	Case dbo.Tenant.Type When 1 Then dbo.Tenant.CompanyName When 2 Then dbo.Tenant.FirstName + '' '' + dbo.Tenant.MiddleName + '' '' + dbo.Tenant.LastName End As TenantName, 
	dbo.LeaseSignUp_Tenant.TenantPercentage, Isnull(dbo.LeaseSignUp_Tenant.CompanyId, 0) As CompanyId
From dbo.LeaseSignUp_Tenant 
	Inner Join dbo.Tenant ON dbo.LeaseSignUp_Tenant.TenantId = dbo.Tenant.TenantId
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleTenantVisitGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwScheduleTenantVisitGet]  
AS  
Select dbo.ScheduleTenantVisit.ScheduleTenantVisitId, dbo.ScheduleTenantVisit.TenantId, dbo.Tenant.FirstName + '' '' + dbo.Tenant.LastName AS Name,   
	dbo.ScheduleTenantVisit.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, dbo.ScheduleTenantVisit.VisitDate,   
	dbo.ScheduleTenantVisit.VisitTime AS [Visit Time], dbo.ScheduleTenantVisit.Comment, dbo.ScheduleTenantVisit.CreatedOn, dbo.ScheduleTenantVisit.ModifiedOn,   
	dbo.ScheduleTenantVisit.DeletedOn, CONVERT(Varchar(11), dbo.ScheduleTenantVisit.VisitDate, 103) AS [Visit Date],   
	CASE dbo.Property.Type WHEN 1 THEN ''Residential'' WHEN 2 THEN ''Commercial'' END AS [Property Type], dbo.Tenant.FirstName, dbo.Tenant.MiddleName,   
	dbo.ScheduleTenantVisit.MaintenanceCategoryId, dbo.MaintenanceCategory.MaintenanceCategoryName AS [MaintenanceCategory Type], Isnull(dbo.ScheduleTenantVisit.CompanyId, 0) As CompanyId  
From dbo.ScheduleTenantVisit INNER JOIN  
	dbo.Property ON dbo.ScheduleTenantVisit.PropertyId = dbo.Property.PropertyId INNER JOIN  
	dbo.Tenant ON dbo.ScheduleTenantVisit.TenantId = dbo.Tenant.TenantId INNER JOIN  
	dbo.MaintenanceCategory ON dbo.ScheduleTenantVisit.MaintenanceCategoryId = dbo.MaintenanceCategory.MaintenanceCategoryId  
Where (dbo.ScheduleTenantVisit.DeletedOn IS NULL)  
  
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_ChargeGet]'))
EXEC dbo.sp_executesql @statement = N'
Create View [vwLeaseSignUp_ChargeGet] As 
Select dbo.LeaseSignUp_Charge.LeaseSignUp_ChargeId, dbo.LeaseSignUp_Charge.LeaseSignUpId, dbo.LeaseSignUp_Charge.ChargeId, dbo.Charge.ChargeName, 
	dbo.LeaseSignUp_Charge.Mode, dbo.LeaseSignUp_Charge.Amount, Isnull(dbo.LeaseSignUp_Charge.CompanyId, 0) As CompanyId
From dbo.LeaseSignUp_Charge 
	INNER JOIN dbo.Charge ON dbo.LeaseSignUp_Charge.ChargeId = dbo.Charge.ChargeId'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwRequestMaintenanceCategoryGet]'))
EXEC dbo.sp_executesql @statement = N'
  
CREATE View [vwRequestMaintenanceCategoryGet]  
AS  
Select dbo.RequestMaintenanceCategory.RequestMaintenanceCategoryId, dbo.RequestMaintenanceCategory.TenantId, dbo.Tenant.FirstName + '' '' + dbo.Tenant.LastName AS Name,   
                      dbo.RequestMaintenanceCategory.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, dbo.RequestMaintenanceCategory.MaintenanceCategoryDate,   
                      dbo.RequestMaintenanceCategory.Comment, dbo.RequestMaintenanceCategory.CreatedOn, dbo.RequestMaintenanceCategory.ModifiedOn, dbo.RequestMaintenanceCategory.DeletedOn,   
                      CONVERT(Varchar(11), dbo.RequestMaintenanceCategory.MaintenanceCategoryDate, 103) AS [MaintenanceCategory Date],   
                      CASE dbo.Property.Type WHEN 1 THEN ''Residential'' WHEN 2 THEN ''Commercial'' END AS [Property Type], dbo.Tenant.FirstName, dbo.Tenant.MiddleName,   
                      dbo.RequestMaintenanceCategory.RequestInspectionId, Isnull(dbo.RequestMaintenanceCategory.CompanyId, 0) As CompanyId  
From dbo.RequestMaintenanceCategory INNER JOIN  
                      dbo.Property ON dbo.RequestMaintenanceCategory.PropertyId = dbo.Property.PropertyId INNER JOIN  
                      dbo.Tenant ON dbo.RequestMaintenanceCategory.TenantId = dbo.Tenant.TenantId LEFT OUTER JOIN  
                      dbo.RequestInspection ON dbo.RequestMaintenanceCategory.RequestInspectionId = dbo.RequestInspection.RequestInspectionId  
Where (dbo.RequestMaintenanceCategory.DeletedOn IS NULL)  
  
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwRequestInspectionGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwRequestInspectionGet]  
AS  
Select dbo.RequestInspection.RequestInspectionId, dbo.RequestInspection.TenantId, dbo.Tenant.FirstName + '' '' + dbo.Tenant.LastName AS Name,   
	dbo.RequestInspection.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, dbo.RequestInspection.InspectionDate,   
	dbo.RequestInspection.Comment, dbo.RequestInspection.CreatedOn, dbo.RequestInspection.ModifiedOn, dbo.RequestInspection.DeletedOn, CONVERT(Varchar(11),   
	dbo.RequestInspection.InspectionDate, 103) AS [Request Date],   
	CASE dbo.Property.Type WHEN 1 THEN ''Residential'' WHEN 2 THEN ''Commercial'' END AS [Property Type], dbo.Tenant.FirstName, dbo.Tenant.MiddleName, Isnull(dbo.RequestInspection.CompanyId, 0) As CompanyId  
From dbo.RequestInspection INNER JOIN  
	dbo.Property ON dbo.RequestInspection.PropertyId = dbo.Property.PropertyId INNER JOIN  
	dbo.Tenant ON dbo.RequestInspection.TenantId = dbo.Tenant.TenantId  
Where (dbo.RequestInspection.DeletedOn IS NULL)  
  
  
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyTypeGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyTypeGet] As
Select Qry.CompanyId, Qry.PropertyTypeId, Qry.[Property Type], Qry.Description, 
	Case When Len(Qry.ChargeId) > 0 Then Substring(Qry.ChargeId, 1, Len(Qry.ChargeId) - 1) Else Qry.ChargeId End As ChargeId,
	Case When Len(Qry.ChargeName) > 0 Then Substring(Qry.ChargeName, 1, Len(Qry.ChargeName) - 1) Else Qry.ChargeName End As [Charge Name]
From 
(Select P.CompanyId, P.PropertyTypeId, P.PropertyTypeName As [Property Type], P.Description, 
	Isnull(Convert(Varchar(Max), (Select Convert(Varchar(Max), ChargeId) + '','' 
									From Charge_PropertyType CP Where CP.PropertyTypeId = P.PropertyTypeId For XML Path(''''))), '''') As ChargeId,
	Isnull(Convert(Varchar(Max), (Select Convert(Varchar(Max), CM.ChargeName) + '','' 
									From Charge_PropertyType CP Inner Join Charge CM On CP.ChargeId = CM.ChargeId 
									Where CP.PropertyTypeId = P.PropertyTypeId For XML Path(''''))), '''') As ChargeName
From dbo.PropertyType P) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyLocationTypeGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyLocationTypeGet] As
Select dbo.PropertyLocationType.PropertyLocationTypeId, dbo.PropertyLocationType.PropertyId, dbo.Property.PropertyLrNo, 
	dbo.PropertyLocationType.LocationTypeId, dbo.LocationType.LocationTypeName, dbo.PropertyLocationType.LocationValue, Isnull(dbo.PropertyLocationType.CompanyId, 0) As CompanyId
From dbo.PropertyLocationType 
	INNER JOIN dbo.Property ON dbo.PropertyLocationType.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.LocationType ON dbo.PropertyLocationType.LocationTypeId = dbo.LocationType.LocationTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwZoneGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwZoneGet] As 
Select dbo.Zone.ZoneId, dbo.Zone.ZoneName As Zone, dbo.Zone.Description, dbo.Zone.CityId, dbo.City.CityName As City, 
	dbo.City.StateId, dbo.State.StateName As State, dbo.State.CountryId, dbo.Country.CountryName As Country, Isnull(dbo.Zone.CompanyId, 0) As CompanyId
From dbo.Zone 
	INNER JOIN dbo.City ON dbo.Zone.CityId = dbo.City.CityId 
	INNER JOIN dbo.State ON dbo.City.StateId = dbo.State.StateId 
	INNER JOIN dbo.Country ON dbo.State.CountryId = dbo.Country.CountryId'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwTenantGet] As 
Select Qry.CompanyId, Qry.TenantId, Qry.Type, Qry.[Tenant Type], Qry.[First Name], Qry.[Middle Name], Qry.[Last Name], Qry.[Company Name], Qry.[Tenant Name], 
	Case When Len(Qry.MobileNo) > 0 Then Substring(Qry.MobileNo, 1, Len(Qry.MobileNo) - 1) Else Qry.MobileNo End As MobileNo, 
	Case When Len(Qry.EmailId) > 0 Then Substring(Qry.EmailId, 1, Len(Qry.EmailId) - 1) Else Qry.EmailId End As EmailId, 
	PaymentMode, [Payment Mode], Qry.CreatedOn, Qry.ModifiedOn, Qry.DeletedOn
From 
(Select TenantId, Type, Case Type When 1 Then ''Company'' When 2 Then ''Individual'' End As [Tenant Type], FirstName As [First Name], 
	MiddleName As [Middle Name], LastName As [Last Name], CompanyName As [Company Name], 
	Case Type When 1 Then CompanyName When 2 Then FirstName + '' '' + MiddleName + '' '' + LastName End As [Tenant Name], 
	CreatedOn, ModifiedOn, DeletedOn, Isnull(Tenant.CompanyId, 0) As CompanyId, PaymentMode,
	Case PaymentMode When 0 Then '''' When 1 Then ''Monthly'' When 2 Then ''Quarterly'' When 3 Then ''Half yearly'' When 4 Then ''Yearly'' End As [Payment Mode],
	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From TenantCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.TenantId = Tenant.TenantId And CDT.DetailType = 1 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From TenantDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.TenantId = Tenant.TenantId And PDT.DetailType = 1 For XML Path ('''')) 
			End), '''') As MobileNo, 

	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From TenantCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.TenantId = Tenant.TenantId And CDT.DetailType = 2 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From TenantDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.TenantId = Tenant.TenantId And PDT.DetailType = 2 For XML Path (''''))
			End), '''') As EmailId
From Tenant Where DeletedOn Is Null) Qry
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwTenantDetailsGet] As 
Select dbo.TenantDetails.TenantDetailsId, dbo.TenantDetails.TenantId, dbo.TenantDetails.PersonalDetailTypeId, 
	dbo.PersonalDetailType.PersonalDetailTypeName, dbo.TenantDetails.PersonalDetailValue, dbo.PersonalDetailType.DetailType, Isnull(dbo.TenantDetails.CompanyId, 0) As CompanyId
From dbo.TenantDetails 
	INNER JOIN dbo.Tenant ON dbo.TenantDetails.TenantId = dbo.Tenant.TenantId 
	INNER JOIN dbo.PersonalDetailType ON dbo.TenantDetails.PersonalDetailTypeId = dbo.PersonalDetailType.PersonalDetailTypeId'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwTenantCompanyDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwTenantCompanyDetailsGet] As 
Select dbo.TenantCompanyDetails.TenantCompanyDetailsId, dbo.TenantCompanyDetails.TenantId, dbo.TenantCompanyDetails.CompanyDetailTypeId, 
	dbo.CompanyDetailType.CompanyDetailTypeName, dbo.TenantCompanyDetails.CompanyDetailValue, dbo.CompanyDetailType.DetailType, Isnull(dbo.TenantCompanyDetails.CompanyId, 0) As CompanyId
From dbo.TenantCompanyDetails 
	INNER JOIN dbo.Tenant ON dbo.TenantCompanyDetails.TenantId = dbo.Tenant.TenantId 
	INNER JOIN dbo.CompanyDetailType ON dbo.TenantCompanyDetails.CompanyDetailTypeId = dbo.CompanyDetailType.CompanyDetailTypeId
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwStateGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwStateGet]
AS
SELECT     dbo.State.StateId, dbo.State.CountryId, dbo.Country.CountryName AS Country, dbo.State.StateName AS State, dbo.State.Description, Isnull(dbo.State.CompanyId, 0) As CompanyId
FROM         dbo.State INNER JOIN
                      dbo.Country ON dbo.State.CountryId = dbo.Country.CountryId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitPropertyGet]'))
EXEC dbo.sp_executesql @statement = N'  
CREATE View [vwScheduleVisitPropertyGet]  
AS  
Select dbo.ScheduleVisitProperty.ScheduleVisitPropertyId, dbo.ScheduleVisitProperty.ScheduleVisitId, dbo.ScheduleVisitProperty.PropertyId,   
	dbo.Property.PropertyLrNo AS [Property LR No], dbo.ScheduleVisitProperty.Status AS StatusValue,  
	CASE dbo.ScheduleVisitProperty.Status WHEN 1 THEN ''Pending'' WHEN 2 THEN ''Reject'' WHEN 3 THEN ''Cancel'' WHEN 4 THEN ''Postpone'' WHEN 5 THEN ''Confirm'' END AS Status, Isnull(dbo.ScheduleVisitProperty.CompanyId, 0) As CompanyId  
From dbo.ScheduleVisitProperty   
	INNER JOIN dbo.Property ON dbo.ScheduleVisitProperty.PropertyId = dbo.Property.PropertyId  

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SMSCredit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_SMSCredit]
	@SMSCreditId As bigint = 0,
	@CreditNoteNo As nvarchar(Max) = '''',
	@TransactionDate As varchar(Max) = '''',
	@CompanyId As bigint = 0,
	@Quantity As decimal(18, 2) = 0,
	@RatePerSMS As decimal(18, 2) = 0,
	@TaxPercent As decimal(18, 2) = 0,
	@Description As nvarchar(Max) = '''',
	@Opr As Varchar(Max),
	@Return_Value As BigInt = 0 Output 
As
Begin
	Declare @OldQuantity As decimal(18, 2) = 0
	Declare @OldCompanyId As Bigint = 0			
	If @Opr = ''I''
		Begin
			Insert Into SMSCredit
			(CreditNoteNo, TransactionDate, CompanyId, Quantity, RatePerSMS, TaxPercent, Description, CreatedOn)
			Values
			(@CreditNoteNo, @TransactionDate, @CompanyId, @Quantity, @RatePerSMS, @TaxPercent, @Description, GetDate())
			
			Update SMSSettings Set TotalSMS = TotalSMS + @Quantity Where CompanyId = @CompanyId
			
			Set @Return_Value = Scope_Identity()			
		End
	Else If @Opr = ''U''
		Begin
			Select @OldQuantity = Isnull(Quantity, 0), @OldCompanyId = ISNULL(CompanyId, 0) 
			From SMSCredit Where (SMSCreditId = @SMSCreditId)
						
			Update SMSSettings Set TotalSMS = TotalSMS - @OldQuantity Where CompanyId = @OldCompanyId
			
			Update SMSCredit Set 
				CreditNoteNo = @CreditNoteNo, TransactionDate = @TransactionDate, CompanyId = @CompanyId, Quantity = @Quantity, 
				RatePerSMS = @RatePerSMS, TaxPercent = @TaxPercent, Description = @Description
			Where (SMSCreditId = @SMSCreditId)
			
			Update SMSSettings Set TotalSMS = TotalSMS + @Quantity Where CompanyId = @CompanyId
			
			Set @Return_Value = @SMSCreditId
		End
	Else If @Opr = ''D''
		Begin
			Select @OldQuantity = Isnull(Quantity, 0), @OldCompanyId = ISNULL(CompanyId, 0) 
			From SMSCredit Where (SMSCreditId = @SMSCreditId)
						
			Update SMSSettings Set TotalSMS = TotalSMS - @OldQuantity Where CompanyId = @OldCompanyId			
			
			Update SMSCredit Set DeletedOn = GetDate() Where SMSCreditId = @SMSCreditId  
			Set @Return_Value = @SMSCreditId
		End
	Else If @Opr = ''G''
		Begin
			Select SMSCredit.SMSCreditId, SMSCredit.CreditNoteNo, SMSCredit.TransactionDate, SMSCredit.CompanyId, Company.CompanyName, SMSCredit.Quantity, SMSCredit.RatePerSMS, 
				Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) As GrossAmount, SMSCredit.TaxPercent, 
				Convert(decimal(18, 2), ((Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) * SMSCredit.TaxPercent) / 100)) As TaxAmount,				
				Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) + 
				Convert(decimal(18, 2), ((Convert(decimal(18, 2), (SMSCredit.Quantity * SMSCredit.RatePerSMS)) * SMSCredit.TaxPercent) / 100)) As TotalAmount, 
				SMSCredit.Description, SMSCredit.CreatedOn
			From SMSCredit Inner Join Company On SMSCredit.CompanyId = Company.CompanyId
			Where SMSCredit.SMSCreditId = @SMSCreditId And SMSCredit.DeletedOn Is Null
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_State_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_State_Select]
	@Op As nvarchar(Max) = '''',
	@StateName As nvarchar(Max) = '''',
	@StateId As BigInt = 0,
	@CountryId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select StateId From State Where StateName = @StateName And CountryId = @CountryId And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select StateId From State Where StateName = @StateName And StateId <> @StateId And CountryId = @CountryId And CompanyId = @CompanyId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select StateId As [Value], State As [Text] From vwStateGet Where '' +  @Search + '' Order By State Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select StateId,CountryId,Country,State,Description From vwStateGet Where '' +  @Search + '' Order By State Asc'')
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Zone_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Zone_Select]
	@Op As nvarchar(Max) = '''',
	@ZoneName As nvarchar(Max) = '''',
	@ZoneId As BigInt = 0,
	@CityId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select ZoneId From Zone Where ZoneName = @ZoneName And CityId = @CityId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select ZoneId From Zone Where ZoneName = @ZoneName And ZoneId <> @ZoneId And CityId = @CityId
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select ZoneId As [Value], Zone As [Text] From vwZoneGet Where '' +  @Search + '' Order By Zone Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select ZoneId, Zone, CityId, City, StateId, State, CountryId, Country, Description From vwZoneGet Where '' +  @Search + '' Order By Zone Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RecordNoSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_RecordNoSettings]
	@CompanyId As BigInt = 0, 
	@Id As BigInt = 0, 
	@Prefix As varchar(max) = '''',
	@StartingNo As varchar(max) = '''',
	@CLength As BigInt = 0, 
	@Op As Varchar(Max),
	@SearchBy As Varchar(Max) = '''',
	@Return_Value As BigInt = 0 Output  
As  
Begin  
	If @Op = ''D''
		Begin
			Delete From RecordNoSettings Where Id = @Id And CompanyId = @CompanyId
			Set @Return_Value = @Id
		End
	Else If @Op = ''I''  
		Begin  
			If Not Exists (Select 1 From RecordNoSettings Where Id = @Id And CompanyId = @CompanyId)
				Begin
					Insert Into RecordNoSettings 
						(CompanyId, Id, Prefix, StartingNo, CLength)
					Values
						(@CompanyId, @Id, @Prefix, @StartingNo, @CLength)
				End
			Else
				Begin
					Update RecordNoSettings Set 
						Prefix = @Prefix, StartingNo = @StartingNo, CLength = @CLength
					Where Id = @Id And CompanyId = @CompanyId
				End

			Set @Return_Value = 1
		End  
	Else If @Op = ''S''  
		Begin
			If @SearchBy = ''''
				Begin
					Select S.Id, S.Prefix, S.StartingNo, S.CLength, 
						S.Prefix + Replicate(''0'', (S.CLength - Len(S.StartingNo))) + Convert(Varchar(Max), S.StartingNo) As Layout
					From RecordNoSettings S
					Where S.CompanyId = @CompanyId
				End
			Else
				Begin
					Exec (''Select S.Id, S.Prefix, S.StartingNo, S.CLength, 
						S.Prefix + Replicate(''''0'''', (S.CLength - Len(S.StartingNo))) + Convert(Varchar(Max), S.StartingNo) As Layout
					From RecordNoSettings S Where '' + @SearchBy)
				End
		End   
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyType_Select]
	@Op As nvarchar(Max) = '''',
	@PropertyTypeName As nvarchar(Max) = '''',
	@PropertyTypeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select PropertyTypeId From PropertyType Where PropertyTypeName = @PropertyTypeName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select PropertyTypeId From PropertyType Where PropertyTypeName = @PropertyTypeName And PropertyTypeId <> @PropertyTypeId And CompanyId = @CompanyId	
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select PropertyTypeId As [Value], [Property Type] As [Text] From vwPropertyTypeGet Where '' +  @Search + '' Order By [Property Type] Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select PropertyTypeId, [Property Type], Description From vwPropertyTypeGet Where '' +  @Search + '' Order By [Property Type] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PersonalDetailType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PersonalDetailType_Select]
	@Op As nvarchar(Max) = '''',
	@PersonalDetailTypeName As nvarchar(Max) = '''',
	@PersonalDetailTypeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select PersonalDetailTypeId From PersonalDetailType Where PersonalDetailTypeName = @PersonalDetailTypeName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select PersonalDetailTypeId From PersonalDetailType Where PersonalDetailTypeName = @PersonalDetailTypeName And PersonalDetailTypeId <> @PersonalDetailTypeId And CompanyId = @CompanyId	
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select PersonalDetailTypeId As [Value], [Personal Detail Type] As [Text] From vwPersonalDetailTypeGet Where '' +  @Search + '' Order By [Personal Detail Type] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select PersonalDetailTypeId, [Personal Detail Type], TextMode, Description, [Detail Type] From vwPersonalDetailTypeGet Where '' +  @Search + '' Order By [Personal Detail Type] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LocationType_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LocationType_Select]
	@Op As nvarchar(Max) = '''',
	@LocationTypeName As nvarchar(Max) = '''',
	@LocationTypeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select LocationTypeId From LocationType Where LocationTypeName = @LocationTypeName And CompanyId = @CompanyId
		End
	Else If @Op = ''CheckNameUpdate''
		Begin
			Select LocationTypeId From LocationType Where LocationTypeName = @LocationTypeName And LocationTypeId <> @LocationTypeId And CompanyId = @CompanyId	
		End
	Else If @Op = ''GD''
		Begin
			Exec (''Select LocationTypeId As [Value], [Location Type] As [Text] From vwLocationTypeGet Where '' +  @Search + '' Order By [Location Type] Asc'')
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select LocationTypeId, [Location Type], DataPickFrom, Description From vwLocationTypeGet Where '' +  @Search + '' Order By [Location Type] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceCategory_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_MaintenanceCategory_Select]
	@Op As nvarchar(Max) = '''',
	@MaintenanceCategoryName As nvarchar(Max) = '''',
	@MaintenanceCategoryId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select MaintenanceCategoryId From MaintenanceCategory Where MaintenanceCategoryName = @MaintenanceCategoryName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select MaintenanceCategoryId From MaintenanceCategory Where MaintenanceCategoryName = @MaintenanceCategoryName And MaintenanceCategoryId <> @MaintenanceCategoryId And CompanyId = @CompanyId
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select MaintenanceCategoryId As [Value], [Maintenance Category] As [Text] From vwMaintenanceCategoryGet Where '' +  @Search + '' Order By [Maintenance Category] Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select MaintenanceCategoryId, [Maintenance Category], Description From vwMaintenanceCategoryGet Where '' +  @Search + '' Order By [Maintenance Category] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceStatus_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_MaintenanceStatus_Select]
	@Op As nvarchar(Max) = '''',
	@MaintenanceStatusName As nvarchar(Max) = '''',
	@MaintenanceStatusId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@Search As nvarchar(Max) = ''''
As
Begin
	If @Search = ''''
		Set @Search = ''1 = 1''

	If @Op = ''CheckNameInsert''
		Begin
			Select MaintenanceStatusId From MaintenanceStatus Where MaintenanceStatusName = @MaintenanceStatusName And CompanyId = @CompanyId
		End
	ELSE If @Op = ''CheckNameUpdate''
		Begin
			Select MaintenanceStatusId From MaintenanceStatus Where MaintenanceStatusName = @MaintenanceStatusName And CompanyId = @CompanyId And MaintenanceStatusId <> @MaintenanceStatusId	
		End
	ELSE If @Op = ''GD''
		Begin
			Exec (''Select MaintenanceStatusId As [Value], [Maintenance Status] As [Text] From vwMaintenanceStatusGet Where '' +  @Search + '' Order By [Maintenance Status] Asc'')
		End
	ELSE If @Op = ''Select''
		Begin
			Exec (''Select MaintenanceStatusId, [Maintenance Status], Description From vwMaintenanceStatusGet Where '' +  @Search + '' Order By [Maintenance Status] Asc'')
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InvStock]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_InvStock]
As
Begin
	If Exists (Select 1 From sys.tables Where name = ''InventoryStock'')
	Begin
		Drop Table InventoryStock
	End

	If Not Exists (Select 1 From sys.tables Where name = ''InventoryStock'')
	Begin
		Create Table InventoryStock (ItemId BigInt, [Item Code] Varchar(Max), [Item Name] Varchar(Max), [Item Group] Varchar(Max), [Qty On Hand] Float)
	End

	Insert Into InventoryStock (ItemId, [Item Code], [Item Name], [Item Group], [Qty On Hand])
	Select StockLink, Code, Description_1, ItemGroup, Qty_On_Hand From StkItem

	Declare @iNum As Int = 0
	Declare @WhseLink As Int = 0
	Declare @Code As Varchar(Max) = ''''
	Declare @sQry As Varchar(Max) = ''''

	Declare curWh Cursor Local for    
	Select WhseLink, Code From WhseMst
	   
	open curWh    
	Fetch next From curWh Into @WhseLink, @Code
	while @@Fetch_Status = 0    
	Begin    
		Set @iNum = @iNum + 1
		
		Set @sQry = ''Alter Table InventoryStock Add ['' + @Code + ''] Float Not Null Constraint DF_ItemStock_ItemStock'' + Convert(Varchar(Max), @iNum) + '' Default 0''
		Exec (@sQry)
		
		Set @sQry = ''Update InventoryStock Set ['' + @Code + ''] = Isnull((Select WHQtyOnHand From WhseStk Where WHWhseID = '' + Convert(Varchar(Max), @WhseLink) + '' And WHStockLink = ItemId), 0)''
		Exec (@sQry)
		
		Fetch next From curWh Into @WhseLink, @Code
	End    
	Close curWh
	Deallocate curWh

	Select * From InventoryStock

	Declare @InvStock As Varchar(Max) = ''''
	Select @InvStock = Convert(Varchar(Max), (Select ''['' + name + ''], '' From sys.columns Where Object_Name(Object_Id) = ''InventoryStock'' Order By Column_Id Asc For XML Path ('''')))
	Set @InvStock = Substring(@InvStock, 1, Len(@InvStock) - 1)

	Select @InvStock As InvStock
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwInspectionFeedbackGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwInspectionFeedbackGet]  
AS  
Select dbo.InspectionFeedback.InspectionFeedbackId, dbo.InspectionFeedback.TenantId, dbo.Tenant.FirstName + '' '' + dbo.Tenant.LastName AS Name,   
	dbo.InspectionFeedback.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, dbo.InspectionFeedback.VisitDate,   
	dbo.InspectionFeedback.Comment, dbo.InspectionFeedback.CreatedOn, dbo.InspectionFeedback.ModifiedOn, dbo.InspectionFeedback.DeletedOn,   
	CONVERT(Varchar(11), dbo.InspectionFeedback.VisitDate, 103) AS [Feedback Date],   
	CASE dbo.Property.Type WHEN 1 THEN ''Residential'' WHEN 2 THEN ''Commercial'' END AS [Property Type], dbo.Tenant.FirstName, dbo.Tenant.MiddleName,   
	dbo.InspectionFeedback.RequestInspectionId, Isnull(dbo.InspectionFeedback.CompanyId, 0) As CompanyId  
From dbo.InspectionFeedback 
	INNER JOIN dbo.Property ON dbo.InspectionFeedback.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.Tenant ON dbo.InspectionFeedback.TenantId = dbo.Tenant.TenantId 
	LEFT OUTER JOIN dbo.RequestInspection ON dbo.InspectionFeedback.RequestInspectionId = dbo.RequestInspection.RequestInspectionId  
Where (dbo.InspectionFeedback.DeletedOn IS NULL)  
  
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwFeatureGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [vwFeatureGet]
AS
SELECT     FeatureId, FeatureName AS Feature, Description, Isnull(dbo.Feature.CompanyId, 0) As CompanyId
FROM         dbo.Feature


'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwEmployeeGet] As 
Select Qry.CompanyId, Qry.EmployeeId, Qry.Type, Qry.[Employee Type], Qry.[First Name], Qry.[Middle Name], Qry.[Last Name], Qry.[Company Name], Qry.[Employee Name], 
	Qry.IsSalesPerson, Qry.[Sales Person], Qry.IsPropertyManager, Qry.[Property Manager], Qry.IsCareTacker, Qry.[Care Tracker], Qry.IsEmployee, Qry.[Employee], Qry.IsOthers, Qry.[Others], 
	Case When Len(Qry.MobileNo) > 0 Then Substring(Qry.MobileNo, 1, Len(Qry.MobileNo) - 1) Else Qry.MobileNo End As MobileNo, 
	Case When Len(Qry.EmailId) > 0 Then Substring(Qry.EmailId, 1, Len(Qry.EmailId) - 1) Else Qry.EmailId End As EmailId, 
	Qry.CreatedOn, Qry.ModifiedOn, Qry.DeletedOn
From 
(Select CompanyId, EmployeeId, Type, Case Type When 1 Then ''Company'' When 2 Then ''Individual'' End As [Employee Type], 
	FirstName As [First Name], MiddleName As [Middle Name], LastName As [Last Name], CompanyName As [Company Name], 
	Case Type When 1 Then CompanyName When 2 Then FirstName + '' '' + MiddleName + '' '' + LastName End As [Employee Name], 
	IsSalesPerson, Case IsSalesPerson When 0 Then ''No'' Else ''Yes'' End As [Sales Person], 
	IsPropertyManager, Case IsPropertyManager When 0 Then ''No'' Else ''Yes'' End As [Property Manager], 
	IsCareTacker, Case IsCareTacker When 0 Then ''No'' Else ''Yes'' End As [Care Tracker], 
	IsEmployee, Case IsEmployee When 0 Then ''No'' Else ''Yes'' End As [Employee], 
	IsOthers, Case IsOthers When 0 Then ''No'' Else ''Yes'' End As [Others], CreatedOn, ModifiedOn, DeletedOn,
	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From EmployeeCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.EmployeeId = Employee.EmployeeId And CDT.DetailType = 1 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From EmployeeDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.EmployeeId = Employee.EmployeeId And PDT.DetailType = 1 For XML Path ('''')) 
			End), '''') As MobileNo, 

	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From EmployeeCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.EmployeeId = Employee.EmployeeId And CDT.DetailType = 2 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From EmployeeDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.EmployeeId = Employee.EmployeeId And PDT.DetailType = 2 For XML Path (''''))
			End), '''') As EmailId
From Employee Where DeletedOn Is Null) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwEmployeeDetailsGet] As 
Select dbo.EmployeeDetails.EmployeeDetailsId, dbo.EmployeeDetails.EmployeeId, dbo.EmployeeDetails.PersonalDetailTypeId, 
	dbo.PersonalDetailType.PersonalDetailTypeName, dbo.EmployeeDetails.PersonalDetailValue, dbo.PersonalDetailType.DetailType, Isnull(dbo.EmployeeDetails.CompanyId, 0) As CompanyId
From dbo.EmployeeDetails 
	INNER JOIN dbo.Employee ON dbo.EmployeeDetails.EmployeeId = dbo.Employee.EmployeeId 
	INNER JOIN dbo.PersonalDetailType ON dbo.EmployeeDetails.PersonalDetailTypeId = dbo.PersonalDetailType.PersonalDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwEmployeeCompanyDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwEmployeeCompanyDetailsGet] As 
Select dbo.EmployeeCompanyDetails.EmployeeCompanyDetailsId, dbo.EmployeeCompanyDetails.EmployeeId, dbo.EmployeeCompanyDetails.CompanyDetailTypeId, 
	dbo.CompanyDetailType.CompanyDetailTypeName, dbo.EmployeeCompanyDetails.CompanyDetailValue, dbo.CompanyDetailType.DetailType, Isnull(dbo.EmployeeCompanyDetails.CompanyId, 0) As CompanyId
From dbo.EmployeeCompanyDetails 
	INNER JOIN dbo.Employee ON dbo.EmployeeCompanyDetails.EmployeeId = dbo.Employee.EmployeeId 
	INNER JOIN dbo.CompanyDetailType ON dbo.EmployeeCompanyDetails.CompanyDetailTypeId = dbo.CompanyDetailType.CompanyDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCountryGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwCountryGet]
AS
SELECT     CountryId, CountryName AS Country, Description, Isnull(dbo.Country.CompanyId, 0) As CompanyId
FROM         dbo.Country

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCompanyDetailTypeGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwCompanyDetailTypeGet] As 
Select CompanyDetailTypeId, CompanyDetailTypeName As [Company Detail Type], TextMode, Description, DetailType, 
	Case DetailType When 0 Then ''N/A'' When 1 Then ''Contact No.'' When 2 Then ''Email Id'' End As [Detail Type], Isnull(dbo.CompanyDetailType.CompanyId, 0) As CompanyId
From dbo.CompanyDetailType

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwCityGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwCityGet]
AS
SELECT     dbo.City.CityId, dbo.City.StateId, dbo.State.StateName AS State, dbo.State.CountryId, dbo.Country.CountryName AS Country, dbo.City.CityName AS City, 
                      dbo.City.Description, Isnull(dbo.City.CompanyId, 0) As CompanyId
FROM         dbo.City INNER JOIN
                      dbo.State ON dbo.City.StateId = dbo.State.StateId INNER JOIN
                      dbo.Country ON dbo.State.CountryId = dbo.Country.CountryId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwChargeGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwChargeGet] As 
Select Qry.CompanyId, Qry.ChargeId, Qry.Charge, Qry.Description, 
	Case When Len(Qry.PropertyTypeId) > 0 Then Substring(Qry.PropertyTypeId, 1, Len(Qry.PropertyTypeId) - 1) Else Qry.PropertyTypeId End As PropertyTypeId,
	Case When Len(Qry.PropertyTypeName) > 0 Then Substring(Qry.PropertyTypeName, 1, Len(Qry.PropertyTypeName) - 1) Else Qry.PropertyTypeName End As [Property Type]
From 
(Select C.CompanyId, C.ChargeId, C.ChargeName As Charge, C.Description, 
	Isnull(Convert(Varchar(Max), (Select Convert(Varchar(Max), PropertyTypeId) + '','' 
									From Charge_PropertyType CP Where CP.ChargeId = C.ChargeId For XML Path(''''))), '''') As PropertyTypeId,
	Isnull(Convert(Varchar(Max), (Select Convert(Varchar(Max), PM.PropertyTypeName) + '','' 
									From Charge_PropertyType CP Inner Join PropertyType PM On CP.PropertyTypeId = PM.PropertyTypeId 
									Where CP.ChargeId = C.ChargeId For XML Path(''''))), '''') As PropertyTypeName
From dbo.Charge C) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBuildingParameterGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwBuildingParameterGet] As 
Select BuildingParameterId, BuildingParameterName AS [Building Parameter], Description, Isnull(dbo.BuildingParameter.CompanyId, 0) As CompanyId
From dbo.BuildingParameter'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwBrokerGet] As 
Select Qry.CompanyId, Qry.BrokerId, Qry.Type, Qry.[Broker Type], Qry.[First Name], Qry.[Middle Name], Qry.[Last Name], Qry.[Company Name], Qry.[Broker Name], 
	Case When Len(Qry.MobileNo) > 0 Then Substring(Qry.MobileNo, 1, Len(Qry.MobileNo) - 1) Else Qry.MobileNo End As MobileNo, 
	Case When Len(Qry.EmailId) > 0 Then Substring(Qry.EmailId, 1, Len(Qry.EmailId) - 1) Else Qry.EmailId End As EmailId, 
	Qry.CreatedOn, Qry.ModifiedOn, Qry.DeletedOn
From 
(Select BrokerId, Type, Case Type When 1 Then ''Company'' When 2 Then ''Individual'' End As [Broker Type], 
	FirstName As [First Name], MiddleName As [Middle Name], LastName As [Last Name], CompanyName As [Company Name],
	Case Type When 1 Then CompanyName When 2 Then FirstName + '' '' + MiddleName + '' '' + LastName End As [Broker Name],  
	CreatedOn, ModifiedOn, DeletedOn, Isnull(Broker.CompanyId, 0) As CompanyId,
	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From BrokerCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.BrokerId = Broker.BrokerId And CDT.DetailType = 1 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From BrokerDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.BrokerId = Broker.BrokerId And PDT.DetailType = 1 For XML Path ('''')) 
			End), '''') As MobileNo, 

	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From BrokerCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.BrokerId = Broker.BrokerId And CDT.DetailType = 2 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From BrokerDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.BrokerId = Broker.BrokerId And PDT.DetailType = 2 For XML Path (''''))
			End), '''') As EmailId
From Broker Where DeletedOn Is Null) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwBrokerDetailsGet] As 
Select dbo.BrokerDetails.BrokerDetailsId, dbo.BrokerDetails.BrokerId, dbo.BrokerDetails.PersonalDetailTypeId, 
	dbo.PersonalDetailType.PersonalDetailTypeName, dbo.BrokerDetails.PersonalDetailValue, dbo.PersonalDetailType.DetailType, Isnull(dbo.BrokerDetails.CompanyId, 0) As CompanyId
From dbo.BrokerDetails 
	INNER JOIN dbo.Broker ON dbo.BrokerDetails.BrokerId = dbo.Broker.BrokerId 
	INNER JOIN dbo.PersonalDetailType ON dbo.BrokerDetails.PersonalDetailTypeId = dbo.PersonalDetailType.PersonalDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwBrokerCompanyDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwBrokerCompanyDetailsGet] As 
Select dbo.BrokerCompanyDetails.BrokerCompanyDetailsId, dbo.BrokerCompanyDetails.BrokerId, dbo.BrokerCompanyDetails.CompanyDetailTypeId, 
	dbo.CompanyDetailType.CompanyDetailTypeName, dbo.BrokerCompanyDetails.CompanyDetailValue, dbo.CompanyDetailType.DetailType, Isnull(dbo.BrokerCompanyDetails.CompanyId, 0) As CompanyId
From dbo.BrokerCompanyDetails 
	INNER JOIN dbo.Broker ON dbo.BrokerCompanyDetails.BrokerId = dbo.Broker.BrokerId 
	INNER JOIN dbo.CompanyDetailType ON dbo.BrokerCompanyDetails.CompanyDetailTypeId = dbo.CompanyDetailType.CompanyDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwAreaGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwAreaGet]
AS
SELECT     dbo.Area.AreaId, dbo.Area.ZoneId, dbo.Zone.ZoneName AS Zone, dbo.Zone.CityId, dbo.City.CityName AS City, dbo.City.StateId, dbo.State.StateName AS State, 
                      dbo.State.CountryId, dbo.Country.CountryName AS Country, dbo.Area.AreaName AS Area, dbo.Area.Description, Isnull(dbo.Area.CompanyId, 0) As CompanyId
FROM         dbo.Area INNER JOIN
                      dbo.Zone ON dbo.Area.ZoneId = dbo.Zone.ZoneId INNER JOIN
                      dbo.City ON dbo.Zone.CityId = dbo.City.CityId INNER JOIN
                      dbo.State ON dbo.City.StateId = dbo.State.StateId INNER JOIN
                      dbo.Country ON dbo.State.CountryId = dbo.Country.CountryId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwAmenityGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwAmenityGet] As
Select AmenityId, AmenityName AS Amenity, AmenityType, Case AmenityType When 1 Then ''Internal'' When 2 Then ''External'' Else '''' End As [Amenity Type], Description, Isnull(dbo.Amenity.CompanyId, 0) As CompanyId
From dbo.Amenity'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwActivityTypeGet]'))
EXEC dbo.sp_executesql @statement = N'Create View [vwActivityTypeGet] As
Select CompanyId, ActivityTypeId, ActivityTypeName AS [Activity Type], Description 
From dbo.ActivityType'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwLawyerGet] As 
Select Qry.CompanyId, Qry.LawyerId, Qry.Type, Qry.[Lawyer Type], Qry.[First Name], Qry.[Middle Name], Qry.[Last Name], Qry.[Company Name], Qry.[Lawyer Name], 
	Case When Len(Qry.MobileNo) > 0 Then Substring(Qry.MobileNo, 1, Len(Qry.MobileNo) - 1) Else Qry.MobileNo End As MobileNo, 
	Case When Len(Qry.EmailId) > 0 Then Substring(Qry.EmailId, 1, Len(Qry.EmailId) - 1) Else Qry.EmailId End As EmailId, 
	Qry.CreatedOn, Qry.ModifiedOn, Qry.DeletedOn
From 
(Select LawyerId, Type, Case Type When 1 Then ''Company'' When 2 Then ''Individual'' End As [Lawyer Type], 
	FirstName As [First Name], MiddleName As [Middle Name], LastName As [Last Name], CompanyName As [Company Name],
	Case Type When 1 Then CompanyName When 2 Then FirstName + '' '' + MiddleName + '' '' + LastName End As [Lawyer Name],  
	CreatedOn, ModifiedOn, DeletedOn, Isnull(Lawyer.CompanyId, 0) As CompanyId,
	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From LawyerCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.LawyerId = Lawyer.LawyerId And CDT.DetailType = 1 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From LawyerDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.LawyerId = Lawyer.LawyerId And PDT.DetailType = 1 For XML Path ('''')) 
			End), '''') As MobileNo, 

	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From LawyerCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.LawyerId = Lawyer.LawyerId And CDT.DetailType = 2 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From LawyerDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.LawyerId = Lawyer.LawyerId And PDT.DetailType = 2 For XML Path (''''))
			End), '''') As EmailId
From Lawyer Where DeletedOn Is Null) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
Create View [vwLawyerDetailsGet] As 
Select dbo.LawyerDetails.LawyerDetailsId, dbo.LawyerDetails.LawyerId, dbo.LawyerDetails.PersonalDetailTypeId, 
	dbo.PersonalDetailType.PersonalDetailTypeName, dbo.LawyerDetails.PersonalDetailValue, dbo.PersonalDetailType.DetailType, Isnull(dbo.LawyerDetails.CompanyId, 0) As CompanyId
From dbo.LawyerDetails 
	INNER JOIN dbo.Lawyer ON dbo.LawyerDetails.LawyerId = dbo.Lawyer.LawyerId 
	INNER JOIN dbo.PersonalDetailType ON dbo.LawyerDetails.PersonalDetailTypeId = dbo.PersonalDetailType.PersonalDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLawyerCompanyDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
Create View [vwLawyerCompanyDetailsGet] As 
Select dbo.LawyerCompanyDetails.LawyerCompanyDetailsId, dbo.LawyerCompanyDetails.LawyerId, dbo.LawyerCompanyDetails.CompanyDetailTypeId, 
	dbo.CompanyDetailType.CompanyDetailTypeName, dbo.LawyerCompanyDetails.CompanyDetailValue, dbo.CompanyDetailType.DetailType, Isnull(dbo.LawyerCompanyDetails.CompanyId, 0) As CompanyId
From dbo.LawyerCompanyDetails 
	INNER JOIN dbo.Lawyer ON dbo.LawyerCompanyDetails.LawyerId = dbo.Lawyer.LawyerId 
	INNER JOIN dbo.CompanyDetailType ON dbo.LawyerCompanyDetails.CompanyDetailTypeId = dbo.CompanyDetailType.CompanyDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwLandlordGet] As 
Select Qry.CompanyId, Qry.LandlordId, Qry.Type, Qry.[Landlord Type], Qry.[First Name], Qry.[Middle Name], Qry.[Last Name], Qry.[Company Name], Qry.[Landlord Name], 
	Case When Len(Qry.MobileNo) > 0 Then Substring(Qry.MobileNo, 1, Len(Qry.MobileNo) - 1) Else Qry.MobileNo End As MobileNo, 
	Case When Len(Qry.EmailId) > 0 Then Substring(Qry.EmailId, 1, Len(Qry.EmailId) - 1) Else Qry.EmailId End As EmailId, 
	Qry.CreatedOn, Qry.ModifiedOn, Qry.DeletedOn
From 
(Select LandlordId, Type, Case Type When 1 Then ''Company'' When 2 Then ''Individual'' End As [Landlord Type], 
	FirstName As [First Name], MiddleName As [Middle Name], LastName As [Last Name], CompanyName As [Company Name], 
	Case Type When 1 Then CompanyName When 2 Then FirstName + '' '' + MiddleName + '' '' + LastName End As [Landlord Name], 
	CreatedOn, ModifiedOn, DeletedOn, Isnull(Landlord.CompanyId, 0) As CompanyId,
	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From LandlordCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.LandlordId = Landlord.LandlordId And CDT.DetailType = 1 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From LandlordDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.LandlordId = Landlord.LandlordId And PDT.DetailType = 1 For XML Path ('''')) 
			End), '''') As MobileNo, 

	Isnull((Case Type 
				When 1 Then (Select CompanyDetailValue + '','' From LandlordCompanyDetails ED Inner Join CompanyDetailType CDT On ED.CompanyDetailTypeId = CDT.CompanyDetailTypeId 
							Where ED.LandlordId = Landlord.LandlordId And CDT.DetailType = 2 For XML Path (''''))
				When 2 Then (Select PersonalDetailValue + '','' From LandlordDetails ED Inner Join PersonalDetailType PDT On ED.PersonalDetailTypeId = PDT.PersonalDetailTypeId 
							Where ED.LandlordId = Landlord.LandlordId And PDT.DetailType = 2 For XML Path (''''))
			End), '''') As EmailId
From Landlord Where DeletedOn Is Null) Qry

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwLandlordDetailsGet] As 
Select dbo.LandlordDetails.LandlordDetailsId, dbo.LandlordDetails.LandlordId, dbo.LandlordDetails.PersonalDetailTypeId, 
	dbo.PersonalDetailType.PersonalDetailTypeName, dbo.LandlordDetails.PersonalDetailValue, dbo.PersonalDetailType.DetailType, Isnull(dbo.LandlordDetails.CompanyId, 0) As CompanyId
From dbo.LandlordDetails 
	INNER JOIN dbo.Landlord ON dbo.LandlordDetails.LandlordId = dbo.Landlord.LandlordId 
	INNER JOIN dbo.PersonalDetailType ON dbo.LandlordDetails.PersonalDetailTypeId = dbo.PersonalDetailType.PersonalDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLandlordCompanyDetailsGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwLandlordCompanyDetailsGet] As 
Select dbo.LandlordCompanyDetails.LandlordCompanyDetailsId, dbo.LandlordCompanyDetails.LandlordId, dbo.LandlordCompanyDetails.CompanyDetailTypeId, 
	dbo.CompanyDetailType.CompanyDetailTypeName, dbo.LandlordCompanyDetails.CompanyDetailValue, dbo.CompanyDetailType.DetailType, Isnull(dbo.LandlordCompanyDetails.CompanyId, 0) As CompanyId
From dbo.LandlordCompanyDetails 
	INNER JOIN dbo.Landlord ON dbo.LandlordCompanyDetails.LandlordId = dbo.Landlord.LandlordId 
	INNER JOIN dbo.CompanyDetailType ON dbo.LandlordCompanyDetails.CompanyDetailTypeId = dbo.CompanyDetailType.CompanyDetailTypeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyFeatureGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyFeatureGet] As
Select dbo.PropertyFeature.PropertyFeatureId, dbo.PropertyFeature.PropertyId, dbo.Property.PropertyLrNo, 
	dbo.PropertyFeature.FeatureId, dbo.Feature.FeatureName, dbo.PropertyFeature.FeatureValue, Isnull(dbo.PropertyFeature.CompanyId, 0) As CompanyId
From dbo.PropertyFeature 
	INNER JOIN dbo.Property ON dbo.PropertyFeature.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.Feature ON dbo.PropertyFeature.FeatureId = dbo.Feature.FeatureId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyChargeGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyChargeGet] As
Select dbo.PropertyCharge.PropertyChargeId, dbo.PropertyCharge.PropertyId, dbo.Property.PropertyLrNo, dbo.PropertyCharge.ChargeId, dbo.Charge.ChargeName, 
	dbo.PropertyCharge.Mode, dbo.PropertyCharge.Amount, Isnull(dbo.PropertyCharge.CompanyId, 0) As CompanyId
From dbo.PropertyCharge 
	INNER JOIN dbo.Property ON dbo.PropertyCharge.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.Charge ON dbo.PropertyCharge.ChargeId = dbo.Charge.ChargeId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyBuildingParameterGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyBuildingParameterGet] As
Select dbo.PropertyBuildingParameter.PropertyBuildingParameterId, dbo.PropertyBuildingParameter.PropertyId, dbo.Property.PropertyLrNo, 
	dbo.PropertyBuildingParameter.BuildingParameterId, dbo.BuildingParameter.BuildingParameterName, dbo.PropertyBuildingParameter.BuildingParameterValue, Isnull(dbo.PropertyBuildingParameter.CompanyId, 0) As CompanyId
From dbo.PropertyBuildingParameter 
	INNER JOIN dbo.Property ON dbo.PropertyBuildingParameter.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.BuildingParameter ON dbo.PropertyBuildingParameter.BuildingParameterId = dbo.BuildingParameter.BuildingParameterId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyAmenityGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPropertyAmenityGet]
AS
Select dbo.PropertyAmenity.PropertyAmenityId, dbo.PropertyAmenity.PropertyId, dbo.Property.PropertyLrNo, dbo.PropertyAmenity.AmenityId, dbo.Amenity.AmenityName, 
	dbo.Amenity.AmenityType, Case dbo.Amenity.AmenityType WHEN 1 THEN ''Internal'' WHEN 2 THEN ''External'' END AS [Amenity Type], Isnull(dbo.PropertyAmenity.CompanyId, 0) As CompanyId
From dbo.PropertyAmenity 
	INNER JOIN dbo.Property ON dbo.PropertyAmenity.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.Amenity ON dbo.PropertyAmenity.AmenityId = dbo.Amenity.AmenityId

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPersonalDetailTypeGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwPersonalDetailTypeGet] As 
Select PersonalDetailTypeId, PersonalDetailTypeName As [Personal Detail Type], TextMode, Description, DetailType, 
	Case DetailType When 0 Then ''N/A'' When 1 Then ''Contact No.'' When 2 Then ''Email Id'' End As [Detail Type], Isnull(dbo.PersonalDetailType.CompanyId, 0) As CompanyId
From dbo.PersonalDetailType'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwMaintenanceStatusGet]'))
EXEC dbo.sp_executesql @statement = N'



CREATE View [vwMaintenanceStatusGet] As
Select MaintenanceStatusId, MaintenanceStatusName AS [Maintenance Status], Description, Isnull(dbo.MaintenanceStatus.CompanyId, 0) As CompanyId
From dbo.MaintenanceStatus



'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwMaintenanceCategoryGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwMaintenanceCategoryGet] As
Select MaintenanceCategoryId, MaintenanceCategoryName AS [Maintenance Category], Description, Isnull(dbo.MaintenanceCategory.CompanyId, 0) As CompanyId
From dbo.MaintenanceCategory
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLocationTypeGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwLocationTypeGet] As 
Select LocationTypeId, LocationTypeName AS [Location Type], DataPickFrom, 
	Case DataPickFrom 
		When 1 Then ''Country'' When 2 Then ''State/County'' When 3 Then ''City'' 
		When 4 Then ''Zone/Town'' When 5 Then ''Area'' Else ''--No Selection--'' 
	End As [Data Pick From], Description, Isnull(dbo.LocationType.CompanyId, 0) As CompanyId
From dbo.LocationType
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUpGet]'))
EXEC dbo.sp_executesql @statement = N'  
CREATE View [vwLeaseSignUpGet]  
AS  
Select dbo.LeaseSignUp.LeaseSignUpId, dbo.LeaseSignUp.LeaseSignUpNo AS [Lease SignUp No], dbo.LeaseSignUp.LeaseSignUpDate, dbo.LeaseSignUp.PropertyId,   
	dbo.Property.PropertyLrNo AS [Property LR No], dbo.LeaseSignUp.FromDate, dbo.LeaseSignUp.ToDate, CONVERT(Varchar(11), dbo.LeaseSignUp.LeaseSignUpDate, 103) AS [Lease SignUp Date], 
	CONVERT(Varchar(11), dbo.LeaseSignUp.FromDate, 103) AS [From Date], CONVERT(Varchar(11), dbo.LeaseSignUp.ToDate, 103) AS [To Date], dbo.LeaseSignUp.SendFeeNote, 
	dbo.LeaseSignUp.SendFeeNote_EmailId, dbo.LeaseSignUp.SendRentRemindEmail, dbo.LeaseSignUp.SendRentRemindEmail_EmailId, dbo.LeaseSignUp.SendRentRemindSMS, 
	dbo.LeaseSignUp.SendRentRemindSMS_ContactNo, dbo.LeaseSignUp.ReminderCount, dbo.LeaseSignUp.ReminderDueDays, dbo.LeaseSignUp.SendRentDelayEmail, dbo.LeaseSignUp.SendRentDelaySMS,   
	dbo.LeaseSignUp.SendRentDelayEmail_EmailId, dbo.LeaseSignUp.SendRentDelaySMS_ContactNo, dbo.LeaseSignUp.DelayCount, dbo.LeaseSignUp.DelayDueDays,   
	dbo.LeaseSignUp.Comment, dbo.LeaseSignUp.CreatedOn, dbo.LeaseSignUp.ModifiedOn, dbo.LeaseSignUp.DeletedOn, dbo.Property.Type,   
	CASE dbo.Property.Type WHEN 1 THEN ''Residential'' WHEN 2 THEN ''Commercial'' END AS [Property Type], dbo.LeaseSignUp.TerminateFlag,   
	dbo.LeaseSignUp.DueDate AS [Due Date], CASE dbo.LeaseSignUp.TerminateFlag WHEN 0 THEN ''Lease Continue'' WHEN 1 THEN ''Completed'' END AS Status,   
	ISNULL(dbo.LeaseSignUp.CompanyId, 0) AS CompanyId, dbo.LeaseSignUp.BrokerId, dbo.LeaseSignUp.BrokerValue, dbo.LeaseSignUp.LawyerId,   
	dbo.LeaseSignUp.LawyerValue  
From dbo.LeaseSignUp 
	INNER JOIN dbo.Property ON dbo.LeaseSignUp.PropertyId = dbo.Property.PropertyId  
Where (dbo.LeaseSignUp.DeletedOn IS NULL)  
    
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwIssueRecordGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwIssueRecordGet]  
AS  
Select dbo.IssueRecord.IssueRecordId, dbo.IssueRecord.CompanyId, dbo.IssueRecord.IssueNumber AS [Record LR No], dbo.IssueRecord.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No],   
	dbo.IssueRecord.RecordDate, CONVERT(Varchar(11), dbo.IssueRecord.RecordDate, 103) AS [Record Date], 
	dbo.IssueRecord.RecordedBy, Case dbo.IssueRecord.RecordedBy When 1 Then ''Employee'' When 2 Then ''Care Taker'' Else ''Other'' End As [Recorded By],
	dbo.IssueRecord.RecordedById, dbo.vwEmployeeGet.[Employee Name], dbo.IssueRecord.RecordName, dbo.IssueRecord.RecordMobile, dbo.IssueRecord.RecordEmail, 
	dbo.IssueRecord.CategoryId, dbo.MaintenanceCategory.MaintenanceCategoryName AS Category, dbo.IssueRecord.Description, 	
	dbo.IssueRecord.CreatedOn, dbo.IssueRecord.ModifiedOn, dbo.IssueRecord.DeletedOn
From dbo.IssueRecord 
	INNER JOIN dbo.Property ON dbo.IssueRecord.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.MaintenanceCategory ON dbo.IssueRecord.CategoryId = dbo.MaintenanceCategory.MaintenanceCategoryId  
	Inner Join dbo.VwEmployeeGet On dbo.IssueRecord.RecordedById = dbo.VwEmployeeGet.EmployeeId  
Where (dbo.IssueRecord.DeletedOn IS NULL)  '
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwIssueAllocationGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwIssueAllocationGet] As
Select dbo.IssueAllocation.IssueAllocateId, dbo.IssueAllocation.IssueRecordId, dbo.IssueAllocation.CompanyId, 
	dbo.IssueRecord.IssueNumber AS [Record LR No], dbo.IssueRecord.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], 
	dbo.IssueAllocation.ResolvedByDate, CONVERT(Varchar(11), dbo.IssueAllocation.ResolvedByDate, 103) AS [Resolve By Date], 	
	dbo.IssueAllocation.AssignTo, Case dbo.IssueAllocation.AssignTo When 1 Then ''Employee'' When 2 Then ''Care Taker'' Else ''Other'' End As [Assign To],	
	dbo.IssueAllocation.AssignToId, dbo.vwEmployeeGet.[Employee Name], dbo.vwEmployeeGet.MobileNo As EmpMobileNo, dbo.vwEmployeeGet.EmailId As EmpEmailId, 
	dbo.IssueAllocation.IsChargeble, dbo.IssueAllocation.Amount, dbo.IssueAllocation.EmailTo, dbo.IssueAllocation.EmailToId, 
	dbo.IssueRecord.CategoryId, dbo.MaintenanceCategory.MaintenanceCategoryName AS Category, dbo.IssueAllocation.Description, 
	dbo.IssueAllocation.StatusId, dbo.MaintenanceStatus.MaintenanceStatusName AS Stage, 
	dbo.IssueAllocation.IsCompleted, CASE WHEN dbo.IssueAllocation.IsCompleted = 1 THEN ''Yes'' ELSE ''No'' END AS Completed, 
	dbo.IssueAllocation.ProgressDate, CONVERT(Varchar(11), dbo.IssueAllocation.ProgressDate, 103) AS [Progress Date], 
	dbo.IssueAllocation.ExpCompDate, CONVERT(Varchar(11), dbo.IssueAllocation.ExpCompDate, 103) AS [Completion Date],
	dbo.IssueAllocation.CreatedOn, dbo.IssueAllocation.ModifiedOn, dbo.IssueAllocation.DeletedOn
From dbo.IssueAllocation 
	INNER JOIN dbo.IssueRecord ON dbo.IssueAllocation.IssueRecordId = dbo.IssueRecord.IssueRecordId 
	INNER JOIN dbo.Property ON dbo.IssueRecord.PropertyId = dbo.Property.PropertyId 
	INNER JOIN dbo.MaintenanceCategory ON dbo.IssueRecord.CategoryId = dbo.MaintenanceCategory.MaintenanceCategoryId 
	LEFT OUTER JOIN dbo.MaintenanceStatus ON dbo.IssueAllocation.StatusId = dbo.MaintenanceStatus.MaintenanceStatusId  
	Inner Join dbo.vwEmployeeGet On dbo.IssueAllocation.AssignToId = dbo.vwEmployeeGet.EmployeeId  
Where (dbo.IssueAllocation.DeletedOn IS NULL)

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Charge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_LeaseSignUp_Charge] 
	@LeaseSignUp_ChargeId As Int = 0,
	@CompanyId As Int = 0,
	@LeaseSignUpId As Int = 0,
	@ChargeId As Int = 0,
	@Mode As Varchar(Max) = '''',
	@Amount As Numeric(18, 2) = 0,
	@Op As Varchar(Max),
	@ReturnValue As Int=0 Output
AS
Begin
	If @Op = ''I''
		Begin
			Insert Into LeaseSignUp_Charge (LeaseSignUpId, ChargeId, Mode, Amount, CompanyId)
			Values (@LeaseSignUpId, @ChargeId, @Mode, @Amount, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	Else If @Op = ''U''
		Begin
			Update LeaseSignUp_Charge Set LeaseSignUpId = @LeaseSignUpId, ChargeId = @ChargeId, Mode = @Mode, Amount = @Amount
			Where LeaseSignUp_ChargeId = @LeaseSignUp_ChargeId
				
			Set @ReturnValue = @LeaseSignUp_ChargeId
		End
	Else If @Op = ''D''
		Begin
			Delete From LeaseSignUp_Charge Where LeaseSignUp_ChargeId = @LeaseSignUp_ChargeId
			Set @ReturnValue = @LeaseSignUp_ChargeId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeaseSignUp_ChargeGet Where LeaseSignUp_ChargeId = @LeaseSignUp_ChargeId  
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LawyerDetails] 
	@LawyerDetailsId int,
	@CompanyId As Int = 0,
	@LawyerId int=0,
	@PersonalDetailTypeId int=0,
	@PersonalDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @PersonalDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into LawyerDetails (LawyerId, PersonalDetailTypeId, [PersonalDetailValue], CompanyId)
			Values (@LawyerId, @PersonalDetailTypeId, @PersonalDetailValue, @CompanyId)
			
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LawyerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	
			
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update LawyerDetails Set  LawyerId= @LawyerId, PersonalDetailTypeId = @PersonalDetailTypeId, [PersonalDetailValue] = @PersonalDetailValue
			Where LawyerDetailsId = @LawyerDetailsId

			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LawyerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	
				
			Set @ReturnValue = @LawyerDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @PersonalDetailTypeId = Isnull(PersonalDetailTypeId, 0) From LawyerDetails Where LawyerDetailsId = @LawyerDetailsId
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LawyerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''''' Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	

			Delete From LawyerDetails Where LawyerDetailsId = @LawyerDetailsId
			Set @ReturnValue = @LawyerDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwLawyerDetailsGet WHERE LawyerDetailsId = @LawyerDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LawyerCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LawyerCompanyDetails] 
	@LawyerCompanyDetailsId int,
	@CompanyId As Int = 0,
	@LawyerId int=0,
	@CompanyDetailTypeId int=0,
	@CompanyDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @CompanyDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into LawyerCompanyDetails (LawyerId, CompanyDetailTypeId, [CompanyDetailValue], CompanyId)
			Values (@LawyerId, @CompanyDetailTypeId, @CompanyDetailValue, @CompanyId)

			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LawyerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + '''''' Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update LawyerCompanyDetails Set  LawyerId= @LawyerId, CompanyDetailTypeId = @CompanyDetailTypeId, [CompanyDetailValue] = @CompanyDetailValue
			Where LawyerCompanyDetailsId = @LawyerCompanyDetailsId
				
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LawyerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + ''''''	 Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	

			Set @ReturnValue = @LawyerCompanyDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @CompanyDetailTypeId = Isnull(CompanyDetailTypeId, 0) From LawyerCompanyDetails Where LawyerCompanyDetailsId = @LawyerCompanyDetailsId
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LawyerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''''' Where LawyerId = '' + Convert(Varchar(Max), @LawyerId)
			Exec (@sQry)	

			Delete From LawyerCompanyDetails Where LawyerCompanyDetailsId = @LawyerCompanyDetailsId
			Set @ReturnValue = @LawyerCompanyDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwLawyerCompanyDetailsGet WHERE LawyerCompanyDetailsId = @LawyerCompanyDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

Create Procedure [usp_MaintenanceStatus] 
	@MaintenanceStatusId Int,
	@CompanyId As Int = 0,
	@MaintenanceStatusName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into MaintenanceStatus (MaintenanceStatusName,[Description], CompanyId)
		Values (@MaintenanceStatusName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update MaintenanceStatus Set MaintenanceStatusName=@MaintenanceStatusName, [Description]=@Description
			Where MaintenanceStatusId = @MaintenanceStatusId
				
			Set @ReturnValue = @MaintenanceStatusId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From MaintenanceStatus Where MaintenanceStatusId = @MaintenanceStatusId
			Set @ReturnValue = @MaintenanceStatusId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwMaintenanceStatusGet Where MaintenanceStatusId = @MaintenanceStatusId
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_MaintenanceCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_MaintenanceCategory] 
	@MaintenanceCategoryId Int,
	@CompanyId As Int = 0,
	@MaintenanceCategoryName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into MaintenanceCategory (MaintenanceCategoryName,[Description], CompanyId)
		Values (@MaintenanceCategoryName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update MaintenanceCategory Set MaintenanceCategoryName=@MaintenanceCategoryName, [Description]=@Description
			Where MaintenanceCategoryId = @MaintenanceCategoryId
				
			Set @ReturnValue = @MaintenanceCategoryId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From MaintenanceCategory Where MaintenanceCategoryId = @MaintenanceCategoryId
			Set @ReturnValue = @MaintenanceCategoryId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwMaintenanceCategoryGet Where MaintenanceCategoryId = @MaintenanceCategoryId
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LocationType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_LocationType] 
	@LocationTypeId Int,
	@CompanyId As Int = 0,
	@LocationTypeName nvarchar(Max)='''',
	@DataPickFrom Int = 0,
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into LocationType (LocationTypeName, DataPickFrom, [Description], CompanyId)
		Values (@LocationTypeName, @DataPickFrom, @Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update LocationType Set LocationTypeName = @LocationTypeName, DataPickFrom = @DataPickFrom, [Description] = @Description
			Where LocationTypeId = @LocationTypeId
				
			Set @ReturnValue = @LocationTypeId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From LocationType Where LocationTypeId = @LocationTypeId
			Set @ReturnValue = @LocationTypeId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwLocationTypeGet Where LocationTypeId = @LocationTypeId
		End	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Tenant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_LeaseSignUp_Tenant] 
	@LeaseSignUp_TenantId As Int = 0,
	@CompanyId As Int = 0,
	@LeaseSignUpId As Int = 0,
	@TenantId As Int = 0,
	@TenantPercentage As Numeric(18, 2) = 0,
	@Op As Varchar(Max),
	@ReturnValue As Int=0 Output
AS
Begin
	If @Op = ''I''
		Begin
			Insert Into LeaseSignUp_Tenant (LeaseSignUpId, TenantId, TenantPercentage, CompanyId)
			Values (@LeaseSignUpId, @TenantId, @TenantPercentage, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	Else If @Op = ''U''
		Begin
			Update LeaseSignUp_Tenant Set LeaseSignUpId = @LeaseSignUpId, TenantId = @TenantId, TenantPercentage = @TenantPercentage
			Where LeaseSignUp_TenantId = @LeaseSignUp_TenantId
				
			Set @ReturnValue = @LeaseSignUp_TenantId
		End
	Else If @Op = ''D''
		Begin
			Delete From LeaseSignUp_Tenant Where LeaseSignUp_TenantId = @LeaseSignUp_TenantId
			Set @ReturnValue = @LeaseSignUp_TenantId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeaseSignUp_TenantGet Where LeaseSignUp_TenantId = @LeaseSignUp_TenantId  
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyBuildingParameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyBuildingParameter] 
	@PropertyBuildingParameterId int,
	@CompanyId As Int = 0,
	@PropertyId int=0,
	@BuildingParameterId int=0,
	@BuildingParameterValue Varchar(Max) = '''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @BuildingParameterName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into PropertyBuildingParameter (PropertyId, BuildingParameterId, [BuildingParameterValue], CompanyId)
			Values (@PropertyId, @BuildingParameterId, @BuildingParameterValue, @CompanyId)

			Select @BuildingParameterName = Isnull(BuildingParameterName, '''') From BuildingParameter Where BuildingParameterId = @BuildingParameterId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @BuildingParameterName + ''] = '''''' + Isnull(@BuildingParameterValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update PropertyBuildingParameter Set PropertyId= @PropertyId, BuildingParameterId = @BuildingParameterId, [BuildingParameterValue] = @BuildingParameterValue
			Where PropertyBuildingParameterId = @PropertyBuildingParameterId
				
			Select @BuildingParameterName = Isnull(BuildingParameterName, '''') From BuildingParameter Where BuildingParameterId = @BuildingParameterId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @BuildingParameterName + ''] = '''''' + Isnull(@BuildingParameterValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = @PropertyBuildingParameterId
		End
	ELSE If @Op = ''D''
		Begin
			Select @BuildingParameterId = Isnull(BuildingParameterId, 0) From PropertyBuildingParameter Where PropertyBuildingParameterId = @PropertyBuildingParameterId
			Select @BuildingParameterName = Isnull(BuildingParameterName, '''') From BuildingParameter Where BuildingParameterId = @BuildingParameterId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @BuildingParameterName + ''] = '''''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	

			Delete From PropertyBuildingParameter Where PropertyBuildingParameterId = @PropertyBuildingParameterId
			Set @ReturnValue = @PropertyBuildingParameterId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwPropertyBuildingParameterGet WHERE PropertyBuildingParameterId = @PropertyBuildingParameterId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyAmenity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyAmenity] 
	@PropertyAmenityId int,
	@CompanyId As Int = 0,
	@PropertyId int=0,
	@AmenityId int=0,
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @AmenityName As Varchar(Max) = ''''
	Declare @AmenityValue As Varchar(Max) = ''''
	Select @AmenityValue = Isnull((Case AmenityType When 1 Then ''Internal'' When 2 Then ''External'' End), '''') From Amenity Where AmenityId = @AmenityId	
	
	If @Op = ''I''
		Begin
			Insert Into PropertyAmenity (PropertyId, AmenityId, CompanyId)
			Values (@PropertyId, @AmenityId, @CompanyId)
				
			Select @AmenityName = Isnull(AmenityName, '''') From Amenity Where AmenityId = @AmenityId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @AmenityName + ''] = '''''' + Isnull(@AmenityValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update PropertyAmenity Set PropertyId = @PropertyId, AmenityId = @AmenityId Where PropertyAmenityId = @PropertyAmenityId

			Select @AmenityName = Isnull(AmenityName, '''') From Amenity Where AmenityId = @AmenityId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @AmenityName + ''] = '''''' + Isnull(@AmenityValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = @PropertyAmenityId
		End
	ELSE If @Op = ''D''
		Begin
			Select @AmenityId = Isnull(AmenityId, 0) From PropertyAmenity Where PropertyAmenityId = @PropertyAmenityId
			Select @AmenityName = Isnull(AmenityName, '''') From Amenity Where AmenityId = @AmenityId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @AmenityName + ''] = '''''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	

			Delete From PropertyAmenity Where PropertyAmenityId = @PropertyAmenityId
			Set @ReturnValue = @PropertyAmenityId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwPropertyAmenityGet WHERE PropertyAmenityId = @PropertyAmenityId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PersonalDetailType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_PersonalDetailType] 
	@PersonalDetailTypeId Int,
	@CompanyId As Int = 0,
	@PersonalDetailTypeName nvarchar(Max)='''',
	@TextMode As Int = 0,
	@Description nvarchar(Max)='''',
	@DetailType As Int = 0,
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into PersonalDetailType (PersonalDetailTypeName, TextMode, [Description], DetailType, CompanyId)
		Values (@PersonalDetailTypeName, @TextMode, @Description, @DetailType, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update PersonalDetailType Set 
				PersonalDetailTypeName = @PersonalDetailTypeName, TextMode = @TextMode, [Description] = @Description, DetailType = @DetailType
			Where PersonalDetailTypeId = @PersonalDetailTypeId
				
			Set @ReturnValue = @PersonalDetailTypeId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @ReturnValue = @PersonalDetailTypeId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwPersonalDetailTypeGet Where PersonalDetailTypeId = @PersonalDetailTypeId
		End	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyType] 
	@PropertyTypeId Int,
	@CompanyId As Int = 0,
	@PropertyTypeName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@ChargeId nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
As
Begin
	If @Op = ''I''
		Begin
			Insert Into PropertyType (PropertyTypeName,[Description], CompanyId)
			Values (@PropertyTypeName,@Description, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
			
			Insert Into Charge_PropertyType (ChargeId, PropertyTypeId)
			Select items, @ReturnValue From dbo.Split(@ChargeId, '','')
		End
	Else If @Op = ''U''
		Begin
			Update PropertyType Set PropertyTypeName=@PropertyTypeName, [Description]=@Description
			Where PropertyTypeId = @PropertyTypeId
				
			Set @ReturnValue = @PropertyTypeId
			
			Delete From Charge_PropertyType Where PropertyTypeId = @PropertyTypeId
			Insert Into Charge_PropertyType (ChargeId, PropertyTypeId)
			Select items, @ReturnValue From dbo.Split(@ChargeId, '','')
		End
	Else If @Op = ''D''
		Begin
			Delete From Charge_PropertyType Where PropertyTypeId = @PropertyTypeId
			Delete From PropertyType Where PropertyTypeId = @PropertyTypeId
			Set @ReturnValue = @PropertyTypeId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwPropertyTypeGet Where PropertyTypeId = @PropertyTypeId
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyLocationType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyLocationType] 
	@PropertyLocationTypeId int,
	@CompanyId As Int = 0,
	@PropertyId As int = 0,
	@LocationTypeId As int = 0,
	@LocationValue AS Varchar(Max) = '''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @LocationTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into PropertyLocationType (PropertyId, LocationTypeId, [LocationValue], CompanyId)
			Values (@PropertyId, @LocationTypeId, @LocationValue, @CompanyId)
				
			Select @LocationTypeName = Isnull(LocationTypeName, '''') From LocationType Where LocationTypeId = @LocationTypeId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @LocationTypeName + ''] = '''''' + Isnull(@LocationValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update PropertyLocationType Set PropertyId= @PropertyId, LocationTypeId = @LocationTypeId, [LocationValue] = @LocationValue
			Where PropertyLocationTypeId = @PropertyLocationTypeId
				
			Select @LocationTypeName = Isnull(LocationTypeName, '''') From LocationType Where LocationTypeId = @LocationTypeId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @LocationTypeName + ''] = '''''' + Isnull(@LocationValue, '''') + ''''''	 Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	

			Set @ReturnValue = @PropertyLocationTypeId
		End
	ELSE If @Op = ''D''
		Begin
			Select @LocationTypeId = Isnull(LocationTypeId, 0) From PropertyLocationType Where PropertyLocationTypeId = @PropertyLocationTypeId
			Select @LocationTypeName = Isnull(LocationTypeName, '''') From LocationType Where LocationTypeId = @LocationTypeId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @LocationTypeName + ''] = '''''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	

			Delete From PropertyLocationType Where PropertyLocationTypeId = @PropertyLocationTypeId
			Set @ReturnValue = @PropertyLocationTypeId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwPropertyLocationTypeGet WHERE PropertyLocationTypeId = @PropertyLocationTypeId  
		END	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyFeature]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyFeature] 
	@PropertyFeatureId int,
	@CompanyId As Int = 0,
	@PropertyId int=0,
	@FeatureId int=0,
	@FeatureValue Varchar(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @FeatureName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into PropertyFeature (PropertyId, FeatureId, [FeatureValue], CompanyId)
			Values (@PropertyId, @FeatureId, @FeatureValue, @CompanyId)

			Select @FeatureName = Isnull(FeatureName, '''') From Feature Where FeatureId = @FeatureId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @FeatureName + ''] = '''''' + Isnull(@FeatureValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update PropertyFeature Set PropertyId= @PropertyId, FeatureId = @FeatureId, [FeatureValue] = @FeatureValue
			Where PropertyFeatureId = @PropertyFeatureId
				
			Select @FeatureName = Isnull(FeatureName, '''') From Feature Where FeatureId = @FeatureId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @FeatureName + ''] = '''''' + Isnull(@FeatureValue, '''') + '''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	
				
			Set @ReturnValue = @PropertyFeatureId
		End
	ELSE If @Op = ''D''
		Begin
			Select @FeatureId = Isnull(FeatureId, 0) From PropertyFeature Where PropertyFeatureId = @PropertyFeatureId
			Select @FeatureName = Isnull(FeatureName, '''') From Feature Where FeatureId = @FeatureId
			Set @sQry = ''Update PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Set ['' + @FeatureName + ''] = '''''''' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)	

			Delete From PropertyFeature Where PropertyFeatureId = @PropertyFeatureId
			Set @ReturnValue = @PropertyFeatureId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwPropertyFeatureGet WHERE PropertyFeatureId = @PropertyFeatureId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_PropertyCharge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_PropertyCharge] 
	@PropertyChargeId int,
	@CompanyId As Int = 0,
	@PropertyId int=0,
	@ChargeId int=0,
	@Mode nVARCHAR(Max)='''',
	@Amount int=0,
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into PropertyCharge(PropertyId, ChargeId, Mode, Amount, CompanyId)
		Values (@PropertyId, @ChargeId, @Mode, @Amount, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update PropertyCharge Set  PropertyId= @PropertyId, ChargeId = @ChargeId, 
				Mode = @Mode, Amount = @Amount
			Where PropertyChargeId = @PropertyChargeId
				
			Set @ReturnValue = @PropertyChargeId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From PropertyCharge Where PropertyChargeId = @PropertyChargeId
			Set @ReturnValue = @PropertyChargeId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwPropertyChargeGet WHERE PropertyChargeId = @PropertyChargeId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestMaintenanceCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_RequestMaintenanceCategory] 
@RequestMaintenanceCategoryId Int,
@CompanyId As Int = 0,
@RequestInspectionId int=0,
@TenantId int=0,
@PropertyId int=0,
@MaintenanceCategoryDate nvarchar(Max)='''',  
@Comment nvarchar(Max)='''',  
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @RequestInspectionId = 0
		Set @RequestInspectionId = NULL

	If @Op = ''I''
		Begin		
			Insert Into RequestMaintenanceCategory
			(RequestInspectionId, TenantId, PropertyId, MaintenanceCategoryDate, Comment, CreatedOn, CompanyId)
			Values 
			(@RequestInspectionId, @TenantId, @PropertyId, @MaintenanceCategoryDate, @Comment, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update RequestMaintenanceCategory Set  
				RequestInspectionId = @RequestInspectionId, TenantId = @TenantId, 
				PropertyId = @PropertyId, MaintenanceCategoryDate = @MaintenanceCategoryDate, 
				Comment = @Comment, ModifiedOn = GETDATE() 
			Where RequestMaintenanceCategoryId = @RequestMaintenanceCategoryId
				
			Set @ReturnValue = @RequestMaintenanceCategoryId
		End 
	Else If @Op = ''D''
		Begin			
			Update RequestMaintenanceCategory Set DeletedOn = GETDATE() 
				Where RequestMaintenanceCategoryId = @RequestMaintenanceCategoryId 
			Set @ReturnValue = @RequestMaintenanceCategoryId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwRequestMaintenanceCategoryGet 
				Where RequestMaintenanceCategoryId = @RequestMaintenanceCategoryId 
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestInspection_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_RequestInspection_Select]
	@Op As nvarchar(Max) = '''',
	@Search As nvarchar(Max) = '''',
	@Days As nvarchar(Max) = '''',
	@ReqIns As INT=0
As
Begin
	If @Op = ''GD''
		Begin
			Exec (''Select RequestInspectionId As [Value], Name As [Text] From vwRequestInspectionGet Where '' +  @Search)
		End
	Else If @Op = ''Select''
		Begin
			Exec (''Select * From vwRequestInspectionGet Where '' +  @Search)
		End
	Else If @Op = ''RequestMaintence''
		Begin
			IF @ReqIns <> 0
				BEGIN
					Select RequestInspectionId As [Value], [Name] + '' - '' + [Property LR No] As [Text] From vwRequestInspectionGet 
						Where RequestInspectionId NOT IN ( Select ISNULL(RequestInspectionId, 0) From vwRequestMaintenanceCategoryGet Where RequestInspectionId <> @ReqIns) 
							AND InspectionDate >= @Days				
				END
			ELSE
				BEGIN
					Select RequestInspectionId As [Value], [Name] + '' - '' + [Property LR No] As [Text] From vwRequestInspectionGet 
						Where RequestInspectionId NOT IN ( Select ISNULL(RequestInspectionId, 0) From vwRequestMaintenanceCategoryGet) 
							AND InspectionDate >= @Days
				END
		End
	Else If @Op = ''InspectionFeedback''
		Begin
			IF @ReqIns <> 0
				BEGIN
					Select RequestInspectionId As [Value], [Name] + '' - '' + [Property LR No] As [Text] From vwRequestInspectionGet 
						Where RequestInspectionId NOT IN ( Select ISNULL(RequestInspectionId, 0) From vwInspectionFeedbackGet Where RequestInspectionId <> @ReqIns) 
							AND InspectionDate >= @Days
				END
			ELSE
				BEGIN
					Select RequestInspectionId As [Value], [Name] + '' - '' + [Property LR No] As [Text] From vwRequestInspectionGet 
						Where RequestInspectionId NOT IN ( Select ISNULL(RequestInspectionId, 0) From vwInspectionFeedbackGet) 
							AND InspectionDate >= @Days
				END
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_RequestInspection]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_RequestInspection] 
@RequestInspectionId Int,
@CompanyId As Int = 0,
@TenantId int=0,
@PropertyId int=0,
@InspectionDate nvarchar(Max)='''',  
@Comment nvarchar(Max)='''',  
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @Op = ''I''
		Begin		
			Insert Into RequestInspection
			(TenantId, PropertyId, InspectionDate, Comment, CreatedOn, CompanyId)
			Values 
			(@TenantId, @PropertyId, @InspectionDate, @Comment, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update RequestInspection Set  
				TenantId = @TenantId, PropertyId = @PropertyId, InspectionDate = @InspectionDate, 
				Comment = @Comment, ModifiedOn = GETDATE() 
			Where RequestInspectionId = @RequestInspectionId
				
			Set @ReturnValue = @RequestInspectionId
		End 
	Else If @Op = ''D''
		Begin			
			Update RequestInspection Set DeletedOn = GETDATE() 
				Where RequestInspectionId = @RequestInspectionId 
			Set @ReturnValue = @RequestInspectionId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwRequestInspectionGet 
				Where RequestInspectionId = @RequestInspectionId 
		End
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleTenantVisit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_ScheduleTenantVisit] 
@ScheduleTenantVisitId Int,
@CompanyId As Int = 0,
@TenantId int=0,
@PropertyId int=0,
@MaintenanceCategoryId int=0,
@VisitDate nvarchar(Max)='''',  
@VisitTime nvarchar(Max)='''',  
@Comment nvarchar(Max)='''',  
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @Op = ''I''
		Begin		
			Insert Into ScheduleTenantVisit
			(TenantId, PropertyId, MaintenanceCategoryId, VisitDate, VisitTime, Comment, CreatedOn, CompanyId)
			Values 
			(@TenantId, @PropertyId, @MaintenanceCategoryId, @VisitDate, @VisitTime, @Comment, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update ScheduleTenantVisit Set  
				TenantId = @TenantId, PropertyId = @PropertyId, MaintenanceCategoryId = @MaintenanceCategoryId, VisitDate = @VisitDate, 
				VisitTime = @VisitTime, Comment = @Comment, ModifiedOn = GETDATE() 
			Where ScheduleTenantVisitId = @ScheduleTenantVisitId
				
			Set @ReturnValue = @ScheduleTenantVisitId
		End 
	Else If @Op = ''D''
		Begin			
			Update ScheduleTenantVisit Set DeletedOn = GETDATE() 
				Where ScheduleTenantVisitId = @ScheduleTenantVisitId 
			Set @ReturnValue = @ScheduleTenantVisitId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwScheduleTenantVisitGet 
				Where ScheduleTenantVisitId = @ScheduleTenantVisitId 
		End
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Zone]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Zone] 
	@ZoneId Int,
	@CompanyId As Int = 0,
	@CityId int=0,
	@ZoneName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into Zone(ZoneName,CityId,[Description], CompanyId)
		Values (@ZoneName,@CityId,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	Else If @Op = ''U''
		Begin
			Update Zone Set  ZoneName= @ZoneName, CityId = @CityId, [Description] = @Description
			Where ZoneId = @ZoneId
				
			Set @ReturnValue = @ZoneId
		End
	Else If @Op = ''D''
		Begin
			Delete From Zone Where ZoneId = @ZoneId
			Set @ReturnValue = @ZoneId
		End
	Else If @Op = ''G''
		Begin
			SELECT * FROM vwZoneGet WHERE ZoneId = @ZoneId
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_TenantDetails] 
	@TenantDetailsId int,
	@CompanyId As Int = 0,
	@TenantId int=0,
	@PersonalDetailTypeId int=0,
	@PersonalDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @PersonalDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into TenantDetails (TenantId, PersonalDetailTypeId, [PersonalDetailValue], CompanyId)
			Values (@TenantId, @PersonalDetailTypeId, @PersonalDetailValue, @CompanyId)
			
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update TenantInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	
			
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update TenantDetails Set  TenantId= @TenantId, PersonalDetailTypeId = @PersonalDetailTypeId, [PersonalDetailValue] = @PersonalDetailValue
			Where TenantDetailsId = @TenantDetailsId

			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update TenantInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	
				
			Set @ReturnValue = @TenantDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @PersonalDetailTypeId = Isnull(PersonalDetailTypeId, 0) From TenantDetails Where TenantDetailsId = @TenantDetailsId
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update TenantInformation Set ['' + @PersonalDetailTypeName + ''] = '''''''' Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	

			Delete From TenantDetails Where TenantDetailsId = @TenantDetailsId
			Set @ReturnValue = @TenantDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwTenantDetailsGet WHERE TenantDetailsId = @TenantDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_TenantCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_TenantCompanyDetails] 
	@TenantCompanyDetailsId int,
	@CompanyId As Int = 0,
	@TenantId int=0,
	@CompanyDetailTypeId int=0,
	@CompanyDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @CompanyDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into TenantCompanyDetails (TenantId, CompanyDetailTypeId, [CompanyDetailValue], CompanyId)
			Values (@TenantId, @CompanyDetailTypeId, @CompanyDetailValue, @CompanyId)

			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update TenantCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + '''''' Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update TenantCompanyDetails Set  TenantId= @TenantId, CompanyDetailTypeId = @CompanyDetailTypeId, [CompanyDetailValue] = @CompanyDetailValue
			Where TenantCompanyDetailsId = @TenantCompanyDetailsId
				
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update TenantCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + ''''''	 Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	

			Set @ReturnValue = @TenantCompanyDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @CompanyDetailTypeId = Isnull(CompanyDetailTypeId, 0) From TenantCompanyDetails Where TenantCompanyDetailsId = @TenantCompanyDetailsId
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update TenantCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''''' Where TenantId = '' + Convert(Varchar(Max), @TenantId)
			Exec (@sQry)	

			Delete From TenantCompanyDetails Where TenantCompanyDetailsId = @TenantCompanyDetailsId
			Set @ReturnValue = @TenantCompanyDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwTenantCompanyDetailsGet WHERE TenantCompanyDetailsId = @TenantCompanyDetailsId  
		END	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Tenant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Tenant] 
	@TenantId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',  
	@LastName nvarchar(Max)='''', 
	@CompanyName nvarchar(Max)='''', 
	@Type Int=0,
	@PaymentMode Int=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
AS
BEGIN	
		
	If @Op = ''I''
	Begin
		Insert Into [Tenant]
		(FirstName, MiddleName, LastName, CompanyName, [Type], PaymentMode, CreatedOn, CompanyId)
		Values 
		(@FirstName, @MiddleName, @LastName, @CompanyName, @Type, @PaymentMode, GetDate(), @CompanyId)
						
		Set @ReturnValue = Scope_Identity()
		
		Insert Into TenantInformation (TenantId, CompanyId) Values (@ReturnValue, @CompanyId)
		Insert Into TenantCompanyInformation (TenantId, CompanyId) Values (@ReturnValue, @CompanyId)
	End
	Else If @Op = ''U''
		Begin
			Delete From TenantInformation Where TenantId= @TenantId
			Delete From TenantCompanyInformation Where TenantId= @TenantId

			Insert Into TenantInformation (TenantId, CompanyId) Values (@TenantId, @CompanyId)
			Insert Into TenantCompanyInformation (TenantId, CompanyId) Values (@TenantId, @CompanyId)

			Update [Tenant] Set
				FirstName = @FirstName,  MiddleName = @MiddleName, LastName = @LastName, 
				CompanyName = @CompanyName, [Type] = @Type, PaymentMode = @PaymentMode, ModifiedOn = GetDate()
			Where TenantId= @TenantId
				
			Set @ReturnValue = @TenantId
		End
	Else If @Op = ''D''
		Begin
			Update [Tenant] Set DeletedOn = GETDATE() Where TenantId = @TenantId 
			Set @ReturnValue = @TenantId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwTenantGet Where TenantId = @TenantId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_State]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_State] 
	@StateId Int,
	@CompanyId As Int = 0,
	@CountryId int=0,
	@StateName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into State(StateName,CountryId,[Description], CompanyId)
		Values (@StateName,@CountryId,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	Else If @Op = ''U''
		Begin
			Update State Set  StateName= @StateName, CountryId = @CountryId, [Description] = @Description 
			Where StateId = @StateId
				
			Set @ReturnValue = @StateId
		End
	Else If @Op = ''D''
		Begin
			Delete From State Where StateId= @StateId
			Set @ReturnValue = @StateId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwStateGet Where StateId = @StateId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisitProperty]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_ScheduleVisitProperty] 
	@ScheduleVisitPropertyId int,
	@CompanyId As Int = 0,
	@ScheduleVisitId int=0,
	@PropertyId int=0,
	@Status smallint=0,
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into ScheduleVisitProperty(ScheduleVisitId, PropertyId, [Status], CompanyId)
		Values (@ScheduleVisitId, @PropertyId, @Status, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update ScheduleVisitProperty Set ScheduleVisitId = @ScheduleVisitId, 
				PropertyId= @PropertyId, [Status] = @Status
			Where ScheduleVisitPropertyId = @ScheduleVisitPropertyId
				
			Set @ReturnValue = @ScheduleVisitPropertyId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From ScheduleVisitProperty Where ScheduleVisitPropertyId = @ScheduleVisitPropertyId
			Set @ReturnValue = @ScheduleVisitPropertyId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwScheduleVisitPropertyGet WHERE ScheduleVisitPropertyId = @ScheduleVisitPropertyId  
		END	
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [vwScheduleVisitGet] As 
Select dbo.ScheduleVisit.ScheduleVisitId, dbo.ScheduleVisit.FirstName AS [First Name], dbo.ScheduleVisit.MiddleName AS [Middle Name], 
	dbo.ScheduleVisit.LastName AS [Last Name], dbo.ScheduleVisit.Mobile, dbo.ScheduleVisit.Email, dbo.ScheduleVisit.VisitDate, 
	dbo.ScheduleVisit.VisitTime AS [Visit Time], dbo.ScheduleVisit.Comment, dbo.ScheduleVisit.CreatedOn, dbo.ScheduleVisit.DeletedOn, dbo.ScheduleVisit.ModifiedOn, 
	dbo.ScheduleVisit.FirstName + '' '' + dbo.ScheduleVisit.LastName AS Name, CONVERT(Varchar(11), dbo.ScheduleVisit.VisitDate, 103) AS [Visit Date], 
	vwEmployeeGet.[Employee Name], dbo.ScheduleVisit.EmployeeId, Isnull(dbo.ScheduleVisit.CompanyId, 0) As CompanyId
From dbo.ScheduleVisit 
	Inner Join dbo.vwEmployeeGet ON dbo.ScheduleVisit.EmployeeId = dbo.vwEmployeeGet.EmployeeId
Where dbo.ScheduleVisit.DeletedOn Is Null'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwScheduleVisitCalendarGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwScheduleVisitCalendarGet] As 
Select Top 100 Percent Row_Number() Over (Order By SV.VisitDate Desc) AS LrNo, SV.ScheduleVisitId, SV.CompanyId, SV.EmployeeId, dbo.vwEmployeeGet.[Employee Name], 
	SVP.PropertyId, P.PropertyLrNo, P.PropertyLrNo + '' '' + SV.Comment As ScheduleDetails, SV.FirstName + '' '' + SV.MiddleName + '' '' + SV.LastName As [Visitor Name], 
	SV.VisitDate, SV.VisitTime, Replace(Convert(Varchar(11), SV.VisitDate, 113), '' '', ''-'') As VisitDateDisp,
	DateAdd(Minute, 0, (Convert(Datetime, Convert(Varchar(11), GetDate(), 113) + '' '' + VisitTime))) As VisitTimeDisp, 
	DateAdd(Minute, 100, (Convert(Datetime, Convert(Varchar(11), GetDate(), 113) + '' '' + VisitTime))) As VisitEndTimeDisp, SV.Mobile, SV.Email, SV.Comment, 	
	dbo.vwEmployeeGet.MobileNo As EmpMobileNo, dbo.vwEmployeeGet.EmailId As EmpEmailId, SV.CreatedOn, SV.DeletedOn, SV.ModifiedOn	
From dbo.ScheduleVisit SV 
	Inner Join dbo.vwEmployeeGet ON SV.EmployeeId = dbo.vwEmployeeGet.EmployeeId  
	Inner Join dbo.ScheduleVisitProperty SVP On SV.ScheduleVisitId = SVP.ScheduleVisitId
	Inner Join dbo.Property P On SVP.PropertyId = P.PropertyId
Where SV.DeletedOn Is Null

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwPropertyGet]'))
EXEC dbo.sp_executesql @statement = N'  
CREATE View [vwPropertyGet] As  
Select dbo.Property.PropertyId,   
	Isnull(dbo.Property.Type, 1) As Type, Isnull(dbo.PropertyType.PropertyTypeName, ''Residential'') As [Property Type],   
	dbo.Property.PostedBy, Case dbo.Property.PostedBy WHEN 1 THEN ''Landlord'' WHEN 2 THEN ''Broker'' END As [Posted By], dbo.Property.ProfileId,   
	Case dbo.Property.PostedBy   
	WHEN 1 THEN vwLandlordGet.[Landlord Name]  
	WHEN 2 THEN vwBrokerGet.[Broker Name]  
	WHEN 3 THEN vwEmployeeget.[Employee Name]  
	END AS [Posted By Name],   
	dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.CreatedOn, dbo.Property.ModifiedOn, dbo.Property.DeletedOn,   
	dbo.Property.Status, CASE dbo.Property.Status WHEN 1 THEN ''No'' WHEN 0 THEN ''Yes'' END AS Available, Isnull(dbo.Property.CompanyId, 0) As CompanyId,  
	dbo.Property.PropertyFor, Case dbo.Property.PropertyFor When 1 Then ''For Rent'' When 2 Then ''For Sale'' Else '''' End As [Property For],   
	dbo.Property.PropertyValue As [Property Value], Isnull(dbo.Property.SalesPersonId, 0) As SalesPersonId, Isnull(SalPerson.[Employee Name], '''') As [Sales Person Name]  
From dbo.Property   
	Left Outer Join vwLandlordGet ON dbo.Property.ProfileId = vwLandlordGet.LandlordId   
	Left Outer Join vwBrokerGet ON dbo.Property.ProfileId = vwBrokerGet.BrokerId  
	Left Outer Join vwEmployeeget ON dbo.Property.ProfileId = vwEmployeeget.EmployeeId  
	Left Outer Join dbo.PropertyType ON dbo.Property.Type = dbo.PropertyType.PropertyTypeId  
	Left Outer Join vwEmployeeGet SalPerson ON dbo.Property.SalesPersonId = SalPerson.EmployeeId  
Where dbo.Property.DeletedOn Is Null  
    
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeasePaymentGet]'))
EXEC dbo.sp_executesql @statement = N'  
CREATE View [vwLeasePaymentGet] As  
Select dbo.LeasePayment.LeasePaymentId, dbo.LeasePayment.CompanyId, dbo.LeasePayment.TenantId, dbo.vwTenantGet.[Tenant Name],   
	dbo.LeasePayment.LeaseSignUpId, dbo.LeaseSignUp.LeaseSignUpNo As [Lease SignUp No], dbo.LeaseSignUp.LeaseSignUpDate As [Lease SignUp Date],   
	dbo.LeaseSignUp.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, PropertyType.PropertyTypeName As [Property Type],   
	dbo.LeasePayment.DueDate, CONVERT(Varchar(11), dbo.LeasePayment.DueDate, 103) AS [Due Date], Lease_Charge.InvoiceAmount,   
	dbo.LeasePayment.InvoiceId, dbo.LeasePayment.InvoiceStatus, Case dbo.LeasePayment.InvoiceStatus When 0 Then ''Pending'' When 1 Then ''Processed'' End As [Invoice Status],   
	dbo.LeasePayment.PaymentId, dbo.LeasePayment.PaymentStatus, Case dbo.LeasePayment.PaymentStatus When 0 Then ''Pending'' When 1 Then ''Saved'' When 2 Then ''Processed'' End As [Payment Status],   
	dbo.LeasePayment.TerminateFlag, dbo.LeasePayment.Comment,   
	dbo.LeasePayment.InvoiceDate, CONVERT(Varchar(11), dbo.LeasePayment.InvoiceDate, 103) AS [Invoice Date],   
	dbo.LeasePayment.PaymentDate, CONVERT(Varchar(11), dbo.LeasePayment.PaymentDate, 103) AS [Payment Date],   
	dbo.LeasePayment.CreatedOn, dbo.LeasePayment.ModifiedOn, dbo.LeasePayment.DeletedOn  
From dbo.LeasePayment   
	Inner Join dbo.LeaseSignUp ON dbo.LeasePayment.LeaseSignUpId = dbo.LeaseSignUp.LeaseSignUpId   
	Inner Join dbo.Property ON dbo.LeaseSignUp.PropertyId = dbo.Property.PropertyId   
	Inner Join dbo.vwTenantGet ON dbo.LeasePayment.TenantId = dbo.vwTenantGet.TenantId   
	Inner Join dbo.PropertyType On dbo.Property.Type = dbo.PropertyType.PropertyTypeId  
	Inner Join (Select LeasePaymentId, Sum(Amount) As InvoiceAmount From LeasePayment_Charge Group By LeasePaymentId) As Lease_Charge On   
	dbo.LeasePayment.LeasePaymentId = Lease_Charge.LeasePaymentId  
Where (dbo.LeaseSignUp.DeletedOn IS NULL) AND (dbo.LeasePayment.TerminateFlag = 0)  
    
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeasePayment_ChargeGet]'))
EXEC dbo.sp_executesql @statement = N'  
CREATE View [vwLeasePayment_ChargeGet] As  
Select dbo.LeasePayment_Charge.LeasePayment_ChargeId, dbo.LeasePayment.LeasePaymentId, dbo.LeasePayment.CompanyId, dbo.LeasePayment.TenantId, dbo.vwTenantGet.[Tenant Name],   
	dbo.LeasePayment.LeaseSignUpId, dbo.LeaseSignUp.LeaseSignUpNo As [Lease SignUp No], dbo.LeaseSignUp.LeaseSignUpDate As [Lease SignUp Date],   
	dbo.LeaseSignUp.PropertyId, dbo.Property.PropertyLrNo AS [Property LR No], dbo.Property.Type, PropertyType.PropertyTypeName As [Property Type],   
	dbo.LeasePayment.DueDate, CONVERT(Varchar(11), dbo.LeasePayment.DueDate, 103) AS [Due Date],   
	Lease_Charge.InvoiceAmount, dbo.LeasePayment_Charge.ChargeId, dbo.Charge.ChargeName As [Charge Name], dbo.LeasePayment_Charge.Amount As [Charge Amount],  
	dbo.LeasePayment.InvoiceId, dbo.LeasePayment.InvoiceStatus, Case dbo.LeasePayment.InvoiceStatus When 0 Then ''Pending'' When 1 Then ''Processed'' End As [Invoice Status],   
	dbo.LeasePayment.PaymentId, dbo.LeasePayment.PaymentStatus, Case dbo.LeasePayment.PaymentStatus When 0 Then ''Pending'' When 1 Then ''Saved'' When 2 Then ''Processed'' End As [Payment Status],   
	dbo.LeasePayment.TerminateFlag, dbo.LeasePayment.Comment,   
	dbo.LeasePayment.InvoiceDate, CONVERT(Varchar(11), dbo.LeasePayment.InvoiceDate, 103) AS [Invoice Date],   
	dbo.LeasePayment.PaymentDate, CONVERT(Varchar(11), dbo.LeasePayment.PaymentDate, 103) AS [Payment Date],   
	dbo.LeasePayment.CreatedOn, dbo.LeasePayment.ModifiedOn, dbo.LeasePayment.DeletedOn  
From dbo.LeasePayment   
	Inner Join dbo.LeaseSignUp ON dbo.LeasePayment.LeaseSignUpId = dbo.LeaseSignUp.LeaseSignUpId   
	Inner Join dbo.Property ON dbo.LeaseSignUp.PropertyId = dbo.Property.PropertyId   
	Inner Join dbo.vwTenantGet ON dbo.LeasePayment.TenantId = dbo.vwTenantGet.TenantId   
	Inner Join dbo.PropertyType On dbo.Property.Type = dbo.PropertyType.PropertyTypeId  
	Inner Join (Select LeasePaymentId, Sum(Amount) As InvoiceAmount From LeasePayment_Charge Group By LeasePaymentId) As Lease_Charge On   
		dbo.LeasePayment.LeasePaymentId = Lease_Charge.LeasePaymentId  
	Inner Join dbo.LeasePayment_Charge On dbo.LeasePayment.LeasePaymentId = dbo.LeasePayment_Charge.LeasePaymentId  
	Inner Join dbo.Charge ON dbo.LeasePayment_Charge.ChargeId = dbo.Charge.ChargeId  
Where (dbo.LeaseSignUp.DeletedOn IS NULL) AND (dbo.LeasePayment.TerminateFlag = 0)  
    
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwSalesActivityGet]'))
EXEC dbo.sp_executesql @statement = N'
  
CREATE View [vwSalesActivityGet] As   
Select SV.SalesActivityId, SV.CompanyId, SV.EmployeeId, vwEmployeeGet.[Employee Name],   
 SV.ActivityDate, Convert(Varchar(11), SV.ActivityDate, 103) AS [Activity Date], SV.ActivityTime AS [Activity Time],   
 dbo.Property.PropertyId, dbo.Property.PropertyLrNo As [Property LR No], dbo.ActivityType.ActivityTypeId, dbo.ActivityType.ActivityTypeName,   
 SV.IsDealComplete, Case SV.IsDealComplete When 0 Then ''No'' Else ''Yes'' End As [Deal Complete], SV.BriefRemarks, SV.Remarks, SV.CreatedOn, SV.DeletedOn, SV.ModifiedOn   
From dbo.SalesActivity SV  
 Inner Join dbo.vwEmployeeGet ON SV.EmployeeId = dbo.vwEmployeeGet.EmployeeId  
 Inner Join dbo.Property ON SV.PropertyId = dbo.Property.PropertyId  
 Inner Join dbo.ActivityType ON SV.ActivityTypeId = dbo.ActivityType.ActivityTypeId  
Where SV.DeletedOn Is Null  
  
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwSalesActivityCalendarGet]'))
EXEC dbo.sp_executesql @statement = N'
CREATE View [vwSalesActivityCalendarGet] As 
Select Top 100 Percent Row_Number() Over (Order By SV.ActivityDate Desc) AS LrNo, SV.SalesActivityId, SV.CompanyId, SV.EmployeeId, vwEmployeeGet.[Employee Name], 
	dbo.Property.PropertyId, dbo.Property.PropertyLrNo As [Property LR No], SV.ActivityDate, SV.ActivityTime, Replace(Convert(Varchar(11), SV.ActivityDate, 113), '' '', ''-'') As ActivityDateDisp,
	DateAdd(Minute, 0, (Convert(Datetime, Convert(Varchar(11), GetDate(), 113) + '' '' + ActivityTime))) As ActivityTimeDisp, 
	DateAdd(Minute, 100, (Convert(Datetime, Convert(Varchar(11), GetDate(), 113) + '' '' + ActivityTime))) As ActivityEndTimeDisp, 	
	dbo.ActivityType.ActivityTypeId, dbo.ActivityType.ActivityTypeName As [Activity Type], dbo.vwEmployeeGet.MobileNo As EmpMobileNo, dbo.vwEmployeeGet.EmailId As EmpEmailId, 
	SV.IsDealComplete, Case SV.IsDealComplete When 0 Then ''No'' Else ''Yes'' End As [Deal Complete], SV.BriefRemarks, SV.Remarks, SV.CreatedOn, SV.DeletedOn, SV.ModifiedOn
From dbo.SalesActivity SV
	Inner Join dbo.vwEmployeeGet ON SV.EmployeeId = dbo.vwEmployeeGet.EmployeeId
	Inner Join dbo.Property ON SV.PropertyId = dbo.Property.PropertyId
	Inner Join dbo.ActivityType ON SV.ActivityTypeId = dbo.ActivityType.ActivityTypeId
Where SV.DeletedOn Is Null

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeaseSignUp_Lawyer](
	[LeaseSignUp_LawyerId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpId] [int] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[LawyerPercentage] [numeric](18, 2) NULL,
 CONSTRAINT [PK_LeaseSignUp_Lawyer] PRIMARY KEY CLUSTERED 
(
	[LeaseSignUp_LawyerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_BrokerDetails] 
	@BrokerDetailsId int,
	@CompanyId As Int = 0,
	@BrokerId int=0,
	@PersonalDetailTypeId int=0,
	@PersonalDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @PersonalDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into BrokerDetails (BrokerId, PersonalDetailTypeId, [PersonalDetailValue], CompanyId)
			Values (@BrokerId, @PersonalDetailTypeId, @PersonalDetailValue, @CompanyId)
			
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update BrokerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	
			
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update BrokerDetails Set  BrokerId= @BrokerId, PersonalDetailTypeId = @PersonalDetailTypeId, [PersonalDetailValue] = @PersonalDetailValue
			Where BrokerDetailsId = @BrokerDetailsId

			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update BrokerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	
				
			Set @ReturnValue = @BrokerDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @PersonalDetailTypeId = Isnull(PersonalDetailTypeId, 0) From BrokerDetails Where BrokerDetailsId = @BrokerDetailsId
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update BrokerInformation Set ['' + @PersonalDetailTypeName + ''] = '''''''' Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	

			Delete From BrokerDetails Where BrokerDetailsId = @BrokerDetailsId
			Set @ReturnValue = @BrokerDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwBrokerDetailsGet WHERE BrokerDetailsId = @BrokerDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BrokerCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_BrokerCompanyDetails] 
	@BrokerCompanyDetailsId int,
	@CompanyId As Int = 0,
	@BrokerId int=0,
	@CompanyDetailTypeId int=0,
	@CompanyDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @CompanyDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into BrokerCompanyDetails (BrokerId, CompanyDetailTypeId, [CompanyDetailValue], CompanyId)
			Values (@BrokerId, @CompanyDetailTypeId, @CompanyDetailValue, @CompanyId)

			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update BrokerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + '''''' Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update BrokerCompanyDetails Set  BrokerId= @BrokerId, CompanyDetailTypeId = @CompanyDetailTypeId, [CompanyDetailValue] = @CompanyDetailValue
			Where BrokerCompanyDetailsId = @BrokerCompanyDetailsId
				
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update BrokerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + ''''''	 Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	

			Set @ReturnValue = @BrokerCompanyDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @CompanyDetailTypeId = Isnull(CompanyDetailTypeId, 0) From BrokerCompanyDetails Where BrokerCompanyDetailsId = @BrokerCompanyDetailsId
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update BrokerCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''''' Where BrokerId = '' + Convert(Varchar(Max), @BrokerId)
			Exec (@sQry)	

			Delete From BrokerCompanyDetails Where BrokerCompanyDetailsId = @BrokerCompanyDetailsId
			Set @ReturnValue = @BrokerCompanyDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwBrokerCompanyDetailsGet WHERE BrokerCompanyDetailsId = @BrokerCompanyDetailsId  
		END	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Broker]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_Broker] 
	@BrokerId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',  
	@LastName nvarchar(Max)='''', 
	@CompanyName nvarchar(Max)='''', 
	@Type Int=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
AS
BEGIN	
		
	If @Op = ''I''
	Begin
		Insert Into [Broker]
		(FirstName, MiddleName, LastName, CompanyName, [Type], CreatedOn, CompanyId)
		Values 
		(@FirstName, @MiddleName, @LastName, @CompanyName, @Type, GetDate(), @CompanyId)
						
		Set @ReturnValue = Scope_Identity()
		
		Insert Into BrokerInformation (BrokerId, CompanyId) Values (@ReturnValue, @CompanyId)
		Insert Into BrokerCompanyInformation (BrokerId, CompanyId) Values (@ReturnValue, @CompanyId)
	End
	Else If @Op = ''U''
		Begin
			Delete From BrokerInformation Where BrokerId= @BrokerId
			Delete From BrokerCompanyInformation Where BrokerId= @BrokerId

			Insert Into BrokerInformation (BrokerId, CompanyId) Values (@BrokerId, @CompanyId)
			Insert Into BrokerCompanyInformation (BrokerId, CompanyId) Values (@BrokerId, @CompanyId)

			Update [Broker] Set
				FirstName = @FirstName,  MiddleName = @MiddleName, LastName = @LastName, 
				CompanyName = @CompanyName, [Type] = @Type, ModifiedOn = GetDate()
			Where BrokerId= @BrokerId
				
			Set @ReturnValue = @BrokerId
		End
	Else If @Op = ''D''
		Begin
			Update [Broker] Set DeletedOn = GETDATE() Where BrokerId = @BrokerId 
			Set @ReturnValue = @BrokerId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwBrokerGet Where BrokerId = @BrokerId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Area]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Area] 
	@AreaId INT,
	@CompanyId As Int = 0,
	@ZoneId INT=0,
	@AreaName NVARCHAR(Max)='''',
	@Description NVARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into Area(AreaName,ZoneId,[Description], CompanyId)
		Values (@AreaName,@ZoneId,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update Area Set  AreaName= @AreaName, ZoneId = @ZoneId, [Description] = @Description
			Where AreaId = @AreaId
				
			Set @ReturnValue = @AreaId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From Area Where AreaId = @AreaId
			Set @ReturnValue = @AreaId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwAreaGet WHERE AreaId = @AreaId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Amenity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Amenity] 
	@AmenityId Int,
	@CompanyId As Int = 0,
	@AmenityName nvarchar(Max)='''',
	@AmenityType smallint = 0,
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into Amenity (AmenityName, AmenityType, [Description], CompanyId)
		Values (@AmenityName, @AmenityType, @Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update Amenity Set AmenityName = @AmenityName, AmenityType = @AmenityType, [Description] = @Description
			Where AmenityId = @AmenityId
				
			Set @ReturnValue = @AmenityId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From Amenity Where AmenityId = @AmenityId
			Set @ReturnValue = @AmenityId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwAmenityGet Where AmenityId = @AmenityId
		End	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ActivityType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_ActivityType] 
	@CompanyId As Int = 0,
	@ActivityTypeId Int,
	@ActivityTypeName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into ActivityType (ActivityTypeName,[Description], CompanyId)
		Values (@ActivityTypeName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update ActivityType Set ActivityTypeName=@ActivityTypeName, [Description]=@Description
			Where ActivityTypeId = @ActivityTypeId
				
			Set @ReturnValue = @ActivityTypeId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From ActivityType Where ActivityTypeId = @ActivityTypeId
			Set @ReturnValue = @ActivityTypeId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwActivityTypeGet Where ActivityTypeId = @ActivityTypeId
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[LeaseSignUp_Broker]') AND type in (N'U'))
BEGIN
CREATE TABLE [LeaseSignUp_Broker](
	[LeaseSignUp_BrokerId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[LeaseSignUpId] [int] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[BrokerPercentage] [numeric](18, 2) NULL,
 CONSTRAINT [PK_LeaseSignUp_Broker] PRIMARY KEY CLUSTERED 
(
	[LeaseSignUp_BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_EmployeeDetails] 
	@EmployeeDetailsId int,
	@CompanyId As Int = 0,
	@EmployeeId int=0,
	@PersonalDetailTypeId int=0,
	@PersonalDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @PersonalDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into EmployeeDetails (EmployeeId, PersonalDetailTypeId, [PersonalDetailValue], CompanyId)
			Values (@EmployeeId, @PersonalDetailTypeId, @PersonalDetailValue, @CompanyId)
			
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update EmployeeInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	
			
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update EmployeeDetails Set  EmployeeId= @EmployeeId, PersonalDetailTypeId = @PersonalDetailTypeId, [PersonalDetailValue] = @PersonalDetailValue
			Where EmployeeDetailsId = @EmployeeDetailsId

			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update EmployeeInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	
				
			Set @ReturnValue = @EmployeeDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @PersonalDetailTypeId = Isnull(PersonalDetailTypeId, 0) From EmployeeDetails Where EmployeeDetailsId = @EmployeeDetailsId
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update EmployeeInformation Set ['' + @PersonalDetailTypeName + ''] = '''''''' Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	

			Delete From EmployeeDetails Where EmployeeDetailsId = @EmployeeDetailsId
			Set @ReturnValue = @EmployeeDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwEmployeeDetailsGet WHERE EmployeeDetailsId = @EmployeeDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_EmployeeCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_EmployeeCompanyDetails] 
	@EmployeeCompanyDetailsId int,
	@CompanyId As Int = 0,
	@EmployeeId int=0,
	@CompanyDetailTypeId int=0,
	@CompanyDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @CompanyDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into EmployeeCompanyDetails (EmployeeId, CompanyDetailTypeId, [CompanyDetailValue], CompanyId)
			Values (@EmployeeId, @CompanyDetailTypeId, @CompanyDetailValue, @CompanyId)

			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update EmployeeCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + '''''' Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update EmployeeCompanyDetails Set  EmployeeId= @EmployeeId, CompanyDetailTypeId = @CompanyDetailTypeId, [CompanyDetailValue] = @CompanyDetailValue
			Where EmployeeCompanyDetailsId = @EmployeeCompanyDetailsId
				
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update EmployeeCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + ''''''	 Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	

			Set @ReturnValue = @EmployeeCompanyDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @CompanyDetailTypeId = Isnull(CompanyDetailTypeId, 0) From EmployeeCompanyDetails Where EmployeeCompanyDetailsId = @EmployeeCompanyDetailsId
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update EmployeeCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''''' Where EmployeeId = '' + Convert(Varchar(Max), @EmployeeId)
			Exec (@sQry)	

			Delete From EmployeeCompanyDetails Where EmployeeCompanyDetailsId = @EmployeeCompanyDetailsId
			Set @ReturnValue = @EmployeeCompanyDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwEmployeeCompanyDetailsGet WHERE EmployeeCompanyDetailsId = @EmployeeCompanyDetailsId  
		END	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Employee]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Employee] 
	@EmployeeId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',  
	@LastName nvarchar(Max)='''', 
	@CompanyName nvarchar(Max)='''', 
	@Type Int=0,
	@IsSalesPerson Int=0,
	@IsPropertyManager Int=0,
	@IsCareTacker Int=0,
	@IsEmployee Int=0,
	@IsOthers Int=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
As
Begin	
	If @Op = ''I''
		Begin
			Insert Into [Employee]
			(CompanyId, FirstName, MiddleName, LastName, CompanyName, [Type], CreatedOn, IsSalesPerson, IsPropertyManager, IsCareTacker, IsEmployee, IsOthers)
			Values 
			(@CompanyId, @FirstName, @MiddleName, @LastName, @CompanyName, @Type, GetDate(), @IsSalesPerson, @IsPropertyManager, @IsCareTacker, @IsEmployee, @IsOthers)
							
			Set @ReturnValue = Scope_Identity()
			
			Insert Into EmployeeInformation (EmployeeId, CompanyId) Values (@ReturnValue, @CompanyId)
			Insert Into EmployeeCompanyInformation (EmployeeId, CompanyId) Values (@ReturnValue, @CompanyId)
		End
	Else If @Op = ''U''
		Begin
			Delete From EmployeeInformation Where EmployeeId= @EmployeeId
			Delete From EmployeeCompanyInformation Where EmployeeId= @EmployeeId

			Insert Into EmployeeInformation (EmployeeId, CompanyId) Values (@EmployeeId, @CompanyId)
			Insert Into EmployeeCompanyInformation (EmployeeId, CompanyId) Values (@EmployeeId, @CompanyId)

			Update [Employee] Set
				FirstName = @FirstName,  MiddleName = @MiddleName, LastName = @LastName, CompanyName = @CompanyName, [Type] = @Type, ModifiedOn = GetDate(),
				IsSalesPerson = @IsSalesPerson, IsPropertyManager = @IsPropertyManager, IsCareTacker = @IsCareTacker, IsEmployee = @IsEmployee, IsOthers = @IsOthers
			Where EmployeeId= @EmployeeId
				
			Set @ReturnValue = @EmployeeId
		End
	Else If @Op = ''D''
		Begin
			Update [Employee] Set DeletedOn = GETDATE() Where EmployeeId = @EmployeeId 
			Set @ReturnValue = @EmployeeId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwEmployeeGet Where EmployeeId = @EmployeeId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Country]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Country] 
	@CountryId Int,
	@CompanyId As Int = 0,
	@CountryName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into Country (CountryName,[Description], CompanyId)
		Values (@CountryName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update Country Set CountryName=@CountryName, [Description]=@Description
			Where CountryId = @CountryId
				
			Set @ReturnValue = @CountryId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From Country Where CountryId = @CountryId
			Set @ReturnValue = @CountryId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwCountryGet Where CountryId = @CountryId
		End
	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_CompanyDetailType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_CompanyDetailType] 
	@CompanyDetailTypeId Int,
	@CompanyId As Int = 0,
	@CompanyDetailTypeName nvarchar(Max)='''',
	@TextMode As Int = 0, 
	@Description nvarchar(Max)='''',
	@DetailType As Int = 0,
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into CompanyDetailType (CompanyDetailTypeName, TextMode, [Description], DetailType, CompanyId)
		Values (@CompanyDetailTypeName, @TextMode, @Description, @DetailType, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update CompanyDetailType Set 
				CompanyDetailTypeName = @CompanyDetailTypeName, TextMode = @TextMode, [Description] = @Description, DetailType = @DetailType
			Where CompanyDetailTypeId = @CompanyDetailTypeId
				
			Set @ReturnValue = @CompanyDetailTypeId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @ReturnValue = @CompanyDetailTypeId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwCompanyDetailTypeGet Where CompanyDetailTypeId = @CompanyDetailTypeId
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_City]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_City] 
	@CityId Int,
	@CompanyId As Int = 0,
	@StateId int=0,
	@CityName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into City(CityName,StateId,[Description], CompanyId)
		Values (@CityName,@StateId,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update City Set  CityName= @CityName, StateId = @StateId, [Description] = @Description
			Where CityId = @CityId
				
			Set @ReturnValue = @CityId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From City Where CityId = @CityId
			Set @ReturnValue = @CityId
		End
	ELSE If @Op = ''G''
		Begin
			SELECT * FROM vwCityGet WHERE CityId = @CityId
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Charge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_Charge] 
	@ChargeId Int,
	@CompanyId As Int = 0,
	@ChargeName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@PropertyTypeId nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
		Begin
			Insert Into Charge (ChargeName,[Description], CompanyId)
			Values (@ChargeName,@Description, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
			
			Insert Into Charge_PropertyType (ChargeId, PropertyTypeId)
			Select @ReturnValue, items From dbo.Split(@PropertyTypeId, '','')
		End
	ELSE If @Op = ''U''
		Begin
			Update Charge Set ChargeName=@ChargeName, [Description]=@Description
			Where ChargeId = @ChargeId
				
			Set @ReturnValue = @ChargeId
			
			Delete From Charge_PropertyType Where ChargeId = @ChargeId			
			Insert Into Charge_PropertyType (ChargeId, PropertyTypeId)
			Select @ReturnValue, items From dbo.Split(@PropertyTypeId, '','')
		End
	ELSE If @Op = ''D''
		Begin
			Delete From Charge_PropertyType Where ChargeId = @ChargeId
			Delete From Charge Where ChargeId = @ChargeId
			Set @ReturnValue = @ChargeId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwChargeGet Where ChargeId = @ChargeId
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_BuildingParameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_BuildingParameter] 
	@BuildingParameterId Int,
	@CompanyId As Int = 0,
	@BuildingParameterName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into BuildingParameter (BuildingParameterName,[Description], CompanyId)
		Values (@BuildingParameterName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update BuildingParameter Set BuildingParameterName=@BuildingParameterName, [Description]=@Description
			Where BuildingParameterId = @BuildingParameterId
				
			Set @ReturnValue = @BuildingParameterId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From BuildingParameter Where BuildingParameterId = @BuildingParameterId
			Set @ReturnValue = @BuildingParameterId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwBuildingParameterGet Where BuildingParameterId = @BuildingParameterId
		End	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Lawyer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_Lawyer] 
	@LawyerId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',  
	@LastName nvarchar(Max)='''', 
	@CompanyName nvarchar(Max)='''', 
	@Type Int=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
AS
BEGIN	
		
	If @Op = ''I''
	Begin
		Insert Into [Lawyer]
		(FirstName, MiddleName, LastName, CompanyName, [Type], CreatedOn, CompanyId)
		Values 
		(@FirstName, @MiddleName, @LastName, @CompanyName, @Type, GetDate(), @CompanyId)
						
		Set @ReturnValue = Scope_Identity()
		
		Insert Into LawyerInformation (LawyerId, CompanyId) Values (@ReturnValue, @CompanyId)
		Insert Into LawyerCompanyInformation (LawyerId, CompanyId) Values (@ReturnValue, @CompanyId)
	End
	Else If @Op = ''U''
		Begin
			Delete From LawyerInformation Where LawyerId= @LawyerId
			Delete From LawyerCompanyInformation Where LawyerId= @LawyerId

			Insert Into LawyerInformation (LawyerId, CompanyId) Values (@LawyerId, @CompanyId)
			Insert Into LawyerCompanyInformation (LawyerId, CompanyId) Values (@LawyerId, @CompanyId)

			Update [Lawyer] Set
				FirstName = @FirstName,  MiddleName = @MiddleName, LastName = @LastName, 
				CompanyName = @CompanyName, [Type] = @Type, ModifiedOn = GetDate()
			Where LawyerId= @LawyerId
				
			Set @ReturnValue = @LawyerId
		End
	Else If @Op = ''D''
		Begin
			Update [Lawyer] Set DeletedOn = GETDATE() Where LawyerId = @LawyerId 
			Set @ReturnValue = @LawyerId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLawyerGet Where LawyerId = @LawyerId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LandlordDetails] 
	@LandlordDetailsId int,
	@CompanyId As Int = 0,
	@LandlordId int=0,
	@PersonalDetailTypeId int=0,
	@PersonalDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @PersonalDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into LandlordDetails (LandlordId, PersonalDetailTypeId, [PersonalDetailValue], CompanyId)
			Values (@LandlordId, @PersonalDetailTypeId, @PersonalDetailValue, @CompanyId)
			
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LandlordInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	
			
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update LandlordDetails Set  LandlordId= @LandlordId, PersonalDetailTypeId = @PersonalDetailTypeId, [PersonalDetailValue] = @PersonalDetailValue
			Where LandlordDetailsId = @LandlordDetailsId

			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LandlordInformation Set ['' + @PersonalDetailTypeName + ''] = '''''' + Isnull(@PersonalDetailValue, '''') + '''''' Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	
				
			Set @ReturnValue = @LandlordDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @PersonalDetailTypeId = Isnull(PersonalDetailTypeId, 0) From LandlordDetails Where LandlordDetailsId = @LandlordDetailsId
			Select @PersonalDetailTypeName = Isnull(PersonalDetailTypeName, '''') From PersonalDetailType Where PersonalDetailTypeId = @PersonalDetailTypeId
			Set @sQry = ''Update LandlordInformation Set ['' + @PersonalDetailTypeName + ''] = '''''''' Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	

			Delete From LandlordDetails Where LandlordDetailsId = @LandlordDetailsId
			Set @ReturnValue = @LandlordDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwLandlordDetailsGet WHERE LandlordDetailsId = @LandlordDetailsId  
		END	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LandlordCompanyDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [usp_LandlordCompanyDetails] 
	@LandlordCompanyDetailsId int,
	@CompanyId As Int = 0,
	@LandlordId int=0,
	@CompanyDetailTypeId int=0,
	@CompanyDetailValue VARCHAR(Max)='''',
	@Op As nVARCHAR(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	Declare @sQry As Varchar(Max) = ''''
	Declare @CompanyDetailTypeName As Varchar(Max) = ''''
	
	If @Op = ''I''
		Begin
			Insert Into LandlordCompanyDetails (LandlordId, CompanyDetailTypeId, [CompanyDetailValue], CompanyId)
			Values (@LandlordId, @CompanyDetailTypeId, @CompanyDetailValue, @CompanyId)

			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LandlordCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + '''''' Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update LandlordCompanyDetails Set  LandlordId= @LandlordId, CompanyDetailTypeId = @CompanyDetailTypeId, [CompanyDetailValue] = @CompanyDetailValue
			Where LandlordCompanyDetailsId = @LandlordCompanyDetailsId
				
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LandlordCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''' + Isnull(@CompanyDetailValue, '''') + ''''''	 Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	

			Set @ReturnValue = @LandlordCompanyDetailsId
		End
	ELSE If @Op = ''D''
		Begin
			Select @CompanyDetailTypeId = Isnull(CompanyDetailTypeId, 0) From LandlordCompanyDetails Where LandlordCompanyDetailsId = @LandlordCompanyDetailsId
			Select @CompanyDetailTypeName = Isnull(CompanyDetailTypeName, '''') From CompanyDetailType Where CompanyDetailTypeId = @CompanyDetailTypeId
			Set @sQry = ''Update LandlordCompanyInformation Set ['' + @CompanyDetailTypeName + ''] = '''''''' Where LandlordId = '' + Convert(Varchar(Max), @LandlordId)
			Exec (@sQry)	

			Delete From LandlordCompanyDetails Where LandlordCompanyDetailsId = @LandlordCompanyDetailsId
			Set @ReturnValue = @LandlordCompanyDetailsId
		End
	ELSE If @Op = ''G''
		BEGIN
			SELECT * FROM vwLandlordCompanyDetailsGet WHERE LandlordCompanyDetailsId = @LandlordCompanyDetailsId  
		END	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Landlord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Landlord] 
	@LandlordId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',  
	@LastName nvarchar(Max)='''', 
	@CompanyName nvarchar(Max)='''', 
	@Type Int=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
AS
BEGIN	
		
	If @Op = ''I''
	Begin
		Insert Into [Landlord]
		(FirstName, MiddleName, LastName, CompanyName, [Type], CreatedOn, CompanyId)
		Values 
		(@FirstName, @MiddleName, @LastName, @CompanyName, @Type, GetDate(), @CompanyId)
						
		Set @ReturnValue = Scope_Identity()
		
		Insert Into LandlordInformation (LandlordId, CompanyId) Values (@ReturnValue, @CompanyId)
		Insert Into LandlordCompanyInformation (LandlordId, CompanyId) Values (@ReturnValue, @CompanyId)
	End
	Else If @Op = ''U''
		Begin
			Delete From LandlordInformation Where LandlordId= @LandlordId
			Delete From LandlordCompanyInformation Where LandlordId= @LandlordId

			Insert Into LandlordInformation (LandlordId, CompanyId) Values (@LandlordId, @CompanyId)
			Insert Into LandlordCompanyInformation (LandlordId, CompanyId) Values (@LandlordId, @CompanyId)

			Update [Landlord] Set
				FirstName = @FirstName,  MiddleName = @MiddleName, LastName = @LastName, 
				CompanyName = @CompanyName, [Type] = @Type, ModifiedOn = GetDate()
			Where LandlordId= @LandlordId
				
			Set @ReturnValue = @LandlordId
		End
	Else If @Op = ''D''
		Begin
			Update [Landlord] Set DeletedOn = GETDATE() Where LandlordId = @LandlordId 
			Set @ReturnValue = @LandlordId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLandlordGet Where LandlordId = @LandlordId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_InspectionFeedback]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_InspectionFeedback] 
@InspectionFeedbackId Int,
@CompanyId As Int = 0,
@RequestInspectionId int=0,
@TenantId int=0,
@PropertyId int=0,
@VisitDate nvarchar(Max)='''',  
@Comment nvarchar(Max)='''',  
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @RequestInspectionId = 0
		Set @RequestInspectionId = NULL

	If @Op = ''I''
		Begin		
			Insert Into InspectionFeedback
			(RequestInspectionId, TenantId, PropertyId, VisitDate, Comment, CreatedOn, CompanyId)
			Values 
			(@RequestInspectionId, @TenantId, @PropertyId, @VisitDate, @Comment, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update InspectionFeedback Set  
				RequestInspectionId = @RequestInspectionId, TenantId = @TenantId, 
				PropertyId = @PropertyId, VisitDate = @VisitDate, 
				Comment = @Comment, ModifiedOn = GETDATE() 
			Where InspectionFeedbackId = @InspectionFeedbackId
				
			Set @ReturnValue = @InspectionFeedbackId
		End 
	Else If @Op = ''D''
		Begin			
			Update InspectionFeedback Set DeletedOn = GETDATE() 
				Where InspectionFeedbackId = @InspectionFeedbackId 
			Set @ReturnValue = @InspectionFeedbackId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwInspectionFeedbackGet 
				Where InspectionFeedbackId = @InspectionFeedbackId 
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Feature]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [usp_Feature] 
	@FeatureId Int,
	@CompanyId As Int = 0,
	@FeatureName nvarchar(Max)='''',
	@Description nvarchar(Max)='''',
	@Op As nVarchar(Max),
	@ReturnValue As int=0 Output
AS
BEGIN
	If @Op = ''I''
	Begin
		Insert Into Feature (FeatureName,[Description], CompanyId)
		Values (@FeatureName,@Description, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	ELSE If @Op = ''U''
		Begin
			Update Feature Set FeatureName=@FeatureName, [Description]=@Description
			Where FeatureId = @FeatureId
				
			Set @ReturnValue = @FeatureId
		End
	ELSE If @Op = ''D''
		Begin
			Delete From Feature Where FeatureId = @FeatureId
			Set @ReturnValue = @FeatureId
		End
	ELSE If @Op = ''G''
		Begin
			Select * From vwFeatureGet Where FeatureId = @FeatureId
		End	
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_IssueRecord] 
@IssueRecordId Int,
@CompanyId As Int = 0,
@IssueNumber nvarchar(Max)='''',  
@PropertyId int=0,
@RecordedBy int=0,
@RecordedById int=0,
@RecordName nvarchar(Max)='''',  
@RecordMobile nvarchar(Max)='''',  
@RecordEmail nvarchar(Max)='''',  
@RecordDate nvarchar(Max)='''',  
@CategoryId int=0,
@Description nvarchar(Max)='''',  
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @Op = ''I''
		Begin		
			Insert Into IssueRecord
			(IssueNumber, PropertyId, RecordedBy, RecordedById, RecordName, RecordMobile, RecordEmail, RecordDate, CategoryId, Description, CreatedOn, CompanyId)
			Values 
			(@IssueNumber, @PropertyId, @RecordedBy, @RecordedById, @RecordName, @RecordMobile, @RecordEmail, @RecordDate, @CategoryId, @Description, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update IssueRecord Set  
				PropertyId = @PropertyId, RecordedBy = @RecordedBy, RecordedById = @RecordedById, 
				RecordName = @RecordName, RecordMobile = @RecordMobile, RecordEmail = @RecordEmail, 
				RecordDate = @RecordDate, CategoryId = @CategoryId, Description = @Description, 
				ModifiedOn = GETDATE() 
			Where IssueRecordId = @IssueRecordId
				
			Set @ReturnValue = @IssueRecordId
		End 
	Else If @Op = ''D''
		Begin			
			Update IssueRecord Set DeletedOn = GETDATE() 
				Where IssueRecordId = @IssueRecordId 
			Set @ReturnValue = @IssueRecordId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwIssueRecordGet 
				Where IssueRecordId = @IssueRecordId 
		End
End


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_IssueAllocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE Procedure [usp_IssueAllocation] 
@IssueAllocateId Int,
@CompanyId As Int = 0,
@IssueRecordId int=0,
@AssignTo int=0,
@AssignToId int=0,
@ResolvedByDate nvarchar(Max)='''',  
@IsChargeble int=0,
@Amount nvarchar(Max)='''',  
@EmailTo int=0,
@EmailToId int=0,
@Description nvarchar(Max)='''',  
@StatusId int=0,
@ProgressDate nvarchar(Max)='''',  
@ExpCompDate nvarchar(Max)='''',  
@IsCompleted int=0,
@Op nvarchar(Max),
@ReturnValue As int=0 Output
AS
BEGIN			
	If @Op = ''I''
		Begin		
			Insert Into IssueAllocation
			(IssueRecordId, AssignTo, AssignToId, ResolvedByDate, IsChargeble, Amount, EmailTo, EmailToId, CreatedOn, CompanyId)
			Values 
			(@IssueRecordId, @AssignTo, @AssignToId, @ResolvedByDate, @IsChargeble, @Amount, @EmailTo, @EmailToId, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update IssueAllocation Set  
				IssueRecordId = @IssueRecordId, AssignTo = @AssignTo, AssignToId = @AssignToId, 
				ResolvedByDate = @ResolvedByDate, IsChargeble = @IsChargeble, Amount = @Amount, 
				EmailTo = @EmailTo, EmailToId = @EmailToId, ModifiedOn = GETDATE() 
			Where IssueAllocateId = @IssueAllocateId
				
			Set @ReturnValue = @IssueAllocateId
		End 
	ELSE If @Op = ''PR''
		Begin
			If @ExpCompDate = ''''
				Set @ExpCompDate = NULL
				
			Update IssueAllocation Set  
				[Description] = @Description, StatusId = @StatusId, ProgressDate = @ProgressDate, 
				ExpCompDate = @ExpCompDate, IsCompleted = @IsCompleted, ModifiedOn = GETDATE() 
			Where IssueAllocateId = @IssueAllocateId
				
			Set @ReturnValue = @IssueAllocateId
		End 
	Else If @Op = ''D''
		Begin			
			Update IssueAllocation Set DeletedOn = GETDATE() 
				Where IssueAllocateId = @IssueAllocateId 
			Set @ReturnValue = @IssueAllocateId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwIssueAllocationGet 
				Where IssueAllocateId = @IssueAllocateId 
		End
End
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_LawyerGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwLeaseSignUp_LawyerGet]
AS
SELECT     LeaseSignUp_LawyerId, LeaseSignUpId, LawyerPercentage, ISNULL(CompanyId, 0) AS CompanyId, PaymentDate, CONVERT(Varchar(11), PaymentDate, 103) 
                      AS [Payment Date]
FROM         dbo.LeaseSignUp_Lawyer

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwLeaseSignUp_BrokerGet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [vwLeaseSignUp_BrokerGet]
AS
SELECT     LeaseSignUp_BrokerId, LeaseSignUpId, BrokerPercentage, ISNULL(CompanyId, 0) AS CompanyId, PaymentDate, CONVERT(Varchar(11), PaymentDate, 103) 
                      AS [Payment Date]
FROM         dbo.LeaseSignUp_Broker

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_ScheduleVisit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_ScheduleVisit] 
	@ScheduleVisitId Int,
	@CompanyId As Int = 0,
	@FirstName nvarchar(Max)='''',  
	@MiddleName nvarchar(Max)='''',
	@LastName nvarchar(Max)='''',  
	@Mobile nvarchar(Max)='''',
	@Email nvarchar(Max)='''',  
	@VisitDate nvarchar(Max)='''',  
	@VisitTime nvarchar(Max)='''',  
	@EmployeeId int=0,
	@Comment nvarchar(Max)='''',  
	@Op nvarchar(Max),
	@ReturnValue As int=0 Output
AS
Begin
	If @Op = ''I''
		Begin		
			Insert Into ScheduleVisit
			(FirstName, MiddleName, LastName, Mobile, Email, VisitDate, VisitTime, EmployeeId, Comment, CreatedOn, CompanyId)
			Values 
			(@FirstName, @MiddleName, @LastName, @Mobile, @Email, @VisitDate, @VisitTime, @EmployeeId, @Comment, GetDate(), @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update ScheduleVisit Set  
				FirstName = @FirstName, MiddleName = @MiddleName, 
				LastName = @LastName, Mobile = @Mobile, Email = @Email, 
				VisitDate = @VisitDate, VisitTime = @VisitTime, 
				EmployeeId = @EmployeeId, Comment = @Comment, ModifiedOn = GETDATE() 
			Where ScheduleVisitId= @ScheduleVisitId
				
			Set @ReturnValue = @ScheduleVisitId
		End 
	Else If @Op = ''D''
		Begin			
			Update ScheduleVisit Set DeletedOn = GETDATE() Where ScheduleVisitId = @ScheduleVisitId 
			Set @ReturnValue = @ScheduleVisitId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwScheduleVisitGet Where ScheduleVisitId = @ScheduleVisitId 
		End
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_SalesActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_SalesActivity] 
	@SalesActivityId Int,
	@CompanyId As Int = 0,
	@EmployeeId int=0,
	@ActivityDate nvarchar(Max)='''',  
	@ActivityTime nvarchar(Max)='''',  		
	@PropertyId int=0,
	@ActivityTypeId int=0,
	@IsDealComplete int=0,
	@Remarks nvarchar(Max)='''',  	
	@BriefRemarks nvarchar(Max)='''',  	
	@Op nvarchar(Max),
	@ReturnValue As int=0 Output
AS
Begin
	If @Op = ''I''
		Begin		
			Insert Into SalesActivity
			(CompanyId, EmployeeId, ActivityDate, ActivityTime, PropertyId, ActivityTypeId, IsDealComplete, Remarks, BriefRemarks, CreatedOn)
			Values 
			(@CompanyId, @EmployeeId, @ActivityDate, @ActivityTime, @PropertyId, @ActivityTypeId, @IsDealComplete, @Remarks, @BriefRemarks, GetDate())			
				
			Set @ReturnValue = Scope_Identity()
		End
	ELSE If @Op = ''U''
		Begin
			Update SalesActivity Set  
				EmployeeId = @EmployeeId, ActivityDate = @ActivityDate, ActivityTime = @ActivityTime, PropertyId = @PropertyId, 
				ActivityTypeId = @ActivityTypeId, IsDealComplete = @IsDealComplete, Remarks = @Remarks, BriefRemarks = @BriefRemarks, ModifiedOn = GETDATE() 
			Where SalesActivityId= @SalesActivityId
				
			Set @ReturnValue = @SalesActivityId
		End 
	Else If @Op = ''D''
		Begin			
			Update SalesActivity Set DeletedOn = GETDATE() Where SalesActivityId = @SalesActivityId 
			Set @ReturnValue = @SalesActivityId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwSalesActivityGet Where SalesActivityId = @SalesActivityId 
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_Property]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [usp_Property] 
	@PropertyId Int,
	@CompanyId As Int = 0,
	@PropertyLrNo nvarchar(Max)='''', 
	@Type smallint=0,
	@PostedBy Int=0,
	@ProfileId Int=0,
	@PropertyFor Int=0, 
	@PropertyValue Numeric(18, 2)=0,
	@SalesPersonId BigInt=0,
	@Op As nVarchar(Max)='''',
	@ReturnValue As int=0 Output
As
Begin
	Declare @sQry As Varchar(Max) = ''''
	If @Op = ''I''
		Begin
			Insert Into [Property] (CompanyId, PropertyLrNo, [Type], PostedBy, ProfileId, CreatedOn, PropertyFor, PropertyValue, SalesPersonId)
			Values (@CompanyId, @PropertyLrNo, @Type, @PostedBy, @ProfileId, GetDate(), @PropertyFor, @PropertyValue, @SalesPersonId)
							
			Set @ReturnValue = Scope_Identity()
			Update RecordNoSettings Set StartingNo = StartingNo + 1 Where Id = 1 And CompanyId = 1
			
			Set @sQry = ''Insert Into PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' (PropertyId, CompanyId) Values ('' + Convert(Varchar(Max), @ReturnValue) + '', '' + Convert(Varchar(Max), @CompanyId) + '')''
			Exec (@sQry)
		End
	Else If @Op = ''U''
		Begin
			Set @sQry = ''Delete From PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' Where PropertyId = '' + Convert(Varchar(Max), @PropertyId)
			Exec (@sQry)
			
			Set @sQry = ''Insert Into PropertyInformation_'' + Convert(Varchar(Max), @CompanyId) + '' (PropertyId, CompanyId) Values ('' + Convert(Varchar(Max), @PropertyId) + '', '' + Convert(Varchar(Max), @CompanyId) + '')''
			Exec (@sQry)
			
			Update [Property] Set 
				PropertyLrNo = @PropertyLrNo, [Type] = @Type, PostedBy = @PostedBy, ProfileId = @ProfileId, ModifiedOn = GetDate(),
				PropertyFor = @PropertyFor, PropertyValue = @PropertyValue, SalesPersonId = @SalesPersonId
			Where PropertyId = @PropertyId
				
			Set @ReturnValue = @PropertyId
		End
	Else If @Op = ''D''
		Begin
			Update [Property] Set DeletedOn = GETDATE() Where PropertyId = @PropertyId 
			Set @ReturnValue = @PropertyId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwPropertyGet Where PropertyId = @PropertyId 
		End	
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment_Charge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LeasePayment_Charge] 
	@LeasePayment_ChargeId As BigInt = 0,
	@CompanyId As BigInt = 0,
	@LeasePaymentId As BigInt = 0,
	@LeaseSignUpId As BigInt = 0,
	@ChargeId As BigInt = 0,
	@Amount As Numeric(18, 2) = 0,
	@Op As nVARCHAR(Max),
	@ReturnValue As BigInt = 0 Output
AS
Begin
	If @Op = ''I''
	Begin
		Insert Into LeasePayment_Charge(LeasePaymentId, LeaseSignUpId, ChargeId, Amount, CompanyId)
		Values (@LeasePaymentId, @LeaseSignUpId, @ChargeId, @Amount, @CompanyId)
			
		Set @ReturnValue = Scope_Identity()
	End
	Else If @Op = ''U''
		Begin
			Update LeasePayment_Charge Set LeasePaymentId= @LeasePaymentId, LeaseSignUpId= @LeaseSignUpId, 
				ChargeId = @ChargeId, Amount = @Amount
			Where LeasePayment_ChargeId = @LeasePayment_ChargeId
				
			Set @ReturnValue = @LeasePayment_ChargeId
		End
	Else If @Op = ''D''
		Begin
			Delete From LeasePayment_Charge Where LeasePayment_ChargeId = @LeasePayment_ChargeId
			Set @ReturnValue = @LeasePayment_ChargeId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeasePayment_ChargeGet Where LeasePayment_ChargeId = @LeasePayment_ChargeId  
		End
	Else If @Op = ''GD''
		Begin
			Select * From vwLeasePayment_ChargeGet Where LeasePaymentId = @LeasePaymentId
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeasePayment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LeasePayment] 
	@LeasePaymentId BigInt = 0,
	@CompanyId As BigInt = 0,
	@LeaseSignUpId As BigInt = 0,
	@TenantId As BigInt = 0,
	@DueDate As nvarchar(Max) = '''',
	@InvoiceStatus As Int = 0,
	@InvoiceId As BigInt = 0,
	@PaymentStatus As Int = 0,
	@PaymentId As BigInt = 0,
	@Comment As nvarchar(Max)='''',
	@InvoiceDate As nvarchar(Max)='''',
	@PaymentDate As nvarchar(Max)='''',
	@Op As nvarchar(Max),
	@ReturnValue As BigInt = 0 Output
AS
Begin
	If @InvoiceDate = ''''
		Set @InvoiceDate = Null
		
	If @PaymentDate = ''''
		Set @PaymentDate = NULL
		
	If @Op = ''I''
		Begin		
			Insert Into LeasePayment
				(CompanyId, LeaseSignUpId, TenantId, DueDate, InvoiceStatus, InvoiceId, PaymentStatus, PaymentId, Comment, InvoiceDate, PaymentDate, CreatedOn)
			Values 
				(@CompanyId, @LeaseSignUpId, @TenantId, @DueDate, @InvoiceStatus, @InvoiceId, @PaymentStatus, @PaymentId, @Comment, @InvoiceDate, @PaymentDate, GetDate())
			
			Set @ReturnValue = Scope_Identity()
		End
	Else If @Op = ''U''
		Begin
			Update LeasePayment Set  
				PaymentStatus = @PaymentStatus, PaymentId = @PaymentId, PaymentDate = @PaymentDate, Comment = @Comment, ModifiedOn = GETDATE() 
			Where LeasePaymentId = @LeasePaymentId
				
			Set @ReturnValue = @LeasePaymentId
		End 
	Else If @Op = ''D''
		Begin
			Delete from LeasePaymentCharge Where LeasePaymentId = @LeasePaymentId
			Update LeasePayment Set DeletedOn = GETDATE() Where LeasePaymentId = @LeasePaymentId 			
			Set @ReturnValue = @LeasePaymentId 
		End
	Else If @Op = ''G''
		Begin
			Select * from vwLeasePaymentGet Where LeasePaymentId = @LeasePaymentId 
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LeaseSignUp] 
    @LeaseSignUpId As BigInt = 0,
    @CompanyId As Int = 0,
    @LeaseSignUpNo As Varchar(Max) = '''',
    @LeaseSignUpDate As Varchar(Max) = Null, 
    @PropertyId As BigInt = 0,
    @FromDate As Varchar(Max) = Null, 
    @ToDate As Varchar(Max) = Null, 
    @DueDate As BigInt = 0,
    @BrokerId As BigInt = 0,
    @BrokerValue As Varchar(Max) = Null, 
    @LawyerId As BigInt = 0,
    @LawyerValue As Varchar(Max) = Null, 
    @SendFeeNote As BigInt = 0,
    @SendFeeNote_EmailId As Varchar(Max) = '''',
    @SendRentRemindEmail As BigInt = 0,
    @SendRentRemindEmail_EmailId As Varchar(Max) = '''',
    @SendRentRemindSMS As BigInt = 0,
    @SendRentRemindSMS_ContactNo As Varchar(Max) = '''',            
    @ReminderCount As BigInt = 0,
    @ReminderDueDays As BigInt = 0,
    @SendRentDelayEmail As BigInt = 0,
    @SendRentDelayEmail_EmailId As Varchar(Max) = '''',
    @SendRentDelaySMS As BigInt = 0,
    @SendRentDelaySMS_ContactNo As Varchar(Max) = '''',
    @DelayCount As BigInt = 0,
    @DelayDueDays As BigInt = 0,
    @Comment As Varchar(Max) = '''',
    @OldLeaseSignUpId As BigInt = 0,
	@Op nVarchar(Max),
	@ReturnValue As Int = 0 Output
As
Begin
	Declare @TPropertyId As Int	= 0
	
	If @BrokerId = 0
		Set @BrokerId = NULL

	If @LawyerId = 0
		Set @LawyerId = NULL

	If @Op = ''I''
		Begin		
			Insert Into LeaseSignUp
				(LeaseSignUpNo, LeaseSignUpDate, PropertyId, FromDate, ToDate, DueDate, BrokerId, BrokerValue, LawyerId, LawyerValue, SendFeeNote, SendFeeNote_EmailId, SendRentRemindEmail, SendRentRemindEmail_EmailId, 
				SendRentRemindSMS, SendRentRemindSMS_ContactNo, ReminderCount, ReminderDueDays, SendRentDelayEmail, SendRentDelayEmail_EmailId, SendRentDelaySMS, 
				SendRentDelaySMS_ContactNo, DelayCount, DelayDueDays, Comment, TerminateFlag, CreatedOn, CompanyId)
			Values 
				(@LeaseSignUpNo, @LeaseSignUpDate, @PropertyId, @FromDate, @ToDate, @DueDate, @BrokerId, @BrokerValue, @LawyerId, @LawyerValue, @SendFeeNote, @SendFeeNote_EmailId, @SendRentRemindEmail, @SendRentRemindEmail_EmailId, 
				@SendRentRemindSMS, @SendRentRemindSMS_ContactNo, @ReminderCount, @ReminderDueDays, @SendRentDelayEmail, @SendRentDelayEmail_EmailId, @SendRentDelaySMS, 
				@SendRentDelaySMS_ContactNo, @DelayCount, @DelayDueDays, @Comment, 0, GetDate(), @CompanyId)
			
			Update Property Set [Status] = 1 Where PropertyId = @PropertyId
			Set @ReturnValue = Scope_Identity()
		End
    Else If @Op = ''R''
        Begin
			Insert Into LeaseSignUp
				(LeaseSignUpNo, LeaseSignUpDate, PropertyId, FromDate, ToDate, DueDate, BrokerId, BrokerValue, LawyerId, LawyerValue, SendFeeNote, SendFeeNote_EmailId, SendRentRemindEmail, SendRentRemindEmail_EmailId, 
				SendRentRemindSMS, SendRentRemindSMS_ContactNo, ReminderCount, ReminderDueDays, SendRentDelayEmail, SendRentDelayEmail_EmailId, SendRentDelaySMS, 
				SendRentDelaySMS_ContactNo, DelayCount, DelayDueDays, Comment, TerminateFlag, CreatedOn, CompanyId)
			Values 
				(@LeaseSignUpNo, @LeaseSignUpDate, @PropertyId, @FromDate, @ToDate, @DueDate, @BrokerId, @BrokerValue, @LawyerId, @LawyerValue, @SendFeeNote, @SendFeeNote_EmailId, @SendRentRemindEmail, @SendRentRemindEmail_EmailId, 
				@SendRentRemindSMS, @SendRentRemindSMS_ContactNo, @ReminderCount, @ReminderDueDays, @SendRentDelayEmail, @SendRentDelayEmail_EmailId, @SendRentDelaySMS, 
				@SendRentDelaySMS_ContactNo, @DelayCount, @DelayDueDays, @Comment, 0, GetDate(), @CompanyId)
			
			Update Property Set [Status] = 1 Where PropertyId = @PropertyId
			Update LeaseSignUp Set TerminateFlag = 1 Where LeaseSignUpId = @OldLeaseSignUpId 
			Update LeasePayment Set TerminateFlag = 1 Where LeaseSignUpId = @OldLeaseSignUpId AND DueDate > @FromDate
			
			Set @ReturnValue = Scope_Identity()
        End 
	Else If @Op = ''U''
		Begin
			Update LeaseSignUp Set 
				LeaseSignUpNo = @LeaseSignUpNo, LeaseSignUpDate = @LeaseSignUpDate, PropertyId = @PropertyId, FromDate = @FromDate, ToDate = @ToDate, 
				DueDate = @DueDate, BrokerId = @BrokerId, BrokerValue = @BrokerValue, LawyerId = @LawyerId, LawyerValue = @LawyerValue, 
				SendFeeNote = @SendFeeNote, SendFeeNote_EmailId = @SendFeeNote_EmailId, SendRentRemindEmail = @SendRentRemindEmail, 
				SendRentRemindEmail_EmailId = @SendRentRemindEmail_EmailId, SendRentRemindSMS = @SendRentRemindSMS, 
				SendRentRemindSMS_ContactNo = @SendRentRemindSMS_ContactNo, ReminderCount = @ReminderCount, ReminderDueDays = @ReminderDueDays, 
				SendRentDelayEmail = @SendRentDelayEmail, SendRentDelayEmail_EmailId = @SendRentDelayEmail_EmailId, SendRentDelaySMS = @SendRentDelaySMS, 
				SendRentDelaySMS_ContactNo = @SendRentDelaySMS_ContactNo, DelayCount = @DelayCount, DelayDueDays = @DelayDueDays, Comment = @Comment, 
				ModIfiedOn = GETDATE() 
			Where LeaseSignUpId = @LeaseSignUpId
				
			Set @ReturnValue = @LeaseSignUpId
		End 
	Else If @Op = ''D''
		Begin			
			Select @TPropertyId = PropertyId From LeaseSignUp Where LeaseSignUpId = @LeaseSignUpId
			
			Update Property Set [Status] = 0 Where PropertyId = @TPropertyId
			Update LeasePayment Set DeletedOn = GETDATE() Where LeaseSignUpId = @LeaseSignUpId 
			Update LeaseSignUp Set DeletedOn = GETDATE() Where LeaseSignUpId = @LeaseSignUpId 
			
			Set @ReturnValue = @LeaseSignUpId 
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeaseSignUpGet Where LeaseSignUpId = @LeaseSignUpId 
		End
	Else If @Op = ''T''
		Begin		
			Select @TPropertyId = PropertyId From LeaseSignUp Where LeaseSignUpId = @LeaseSignUpId
			
			Update Property Set [Status] = 0 Where PropertyId = @TPropertyId

            Declare @LeaseEndDate AS nvarchar(Max)
            Set @LeaseEndDate = CONVERT(nvarchar(Max), GETDATE(), 101)
            
            --Add Refund Entry and Terminate all remaining payment & Lease
            --EXEC usp_SecurityRefund 0, @RequestLeaseId, 1, '''''''', @LeaseEndDate, '''''''', ''''I''''    

			Update LeasePayment Set TerminateFlag = 1 Where LeaseSignUpId = @LeaseSignUpId 
			Update LeaseSignUp Set TerminateFlag = 1 Where LeaseSignUpId = @LeaseSignUpId 

			Set @ReturnValue = @LeaseSignUpId 
		End
End

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[uspLeaseSignUp_Payment_Insert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [uspLeaseSignUp_Payment_Insert]
	@LeaseSignUpId As BigInt = 0,
	@CompanyId As BigInt = 0
As
Begin
	Declare @TenantId As BigInt = 0
	Declare @TenantPercentage As Numeric(18, 2) = 0
	Declare @PaymentMode As Int = 0
	Declare @AmountCalc As Int = 0
	Declare @ChargeOnce As Int = 0
	Declare @ChargeId As Int = 0
	Declare @Mode As varchar(Max) = ''''
	Declare @Amount	As Numeric(18, 2) = 0
	Declare @ChargeAmount As Numeric(18, 2) = 0

	Declare @PayDueDate As Datetime = null
	Declare @FromDate As Datetime = Null
	Declare @ToDate As Datetime = Null
	Declare @DueDate As BigInt = 0
	Declare @ReturnValue As BigInt = 0

	Select @FromDate = FromDate, @ToDate = ToDate, @DueDate = DueDate From LeaseSignUp Where LeaseSignUpId = @LeaseSignUpId

	Declare curLST cursor Local for    
	Select LeaseSignUp_Tenant.TenantId, LeaseSignUp_Tenant.TenantPercentage, Tenant.PaymentMode
	From LeaseSignUp_Tenant Inner Join Tenant On LeaseSignUp_Tenant.TenantId = Tenant.TenantId 
	Where LeaseSignUpId = @LeaseSignUpId
	   
	open curLST    
	Fetch next From curLST Into @TenantId, @TenantPercentage, @PaymentMode
	while @@Fetch_Status = 0    
	Begin    
		Set @ChargeOnce = 0
		Set @PayDueDate = Convert(Datetime, Convert(Varchar(Max), Month(@FromDate)) + ''/'' + Convert(Varchar(Max), @DueDate) + ''/'' + Convert(Varchar(Max), Year(@FromDate)))
		
		While (Convert(Datetime, @PayDueDate) <= Convert(Datetime, @ToDate))
		Begin
			If @PaymentMode = 0 Or @PaymentMode = 1
				Set @AmountCalc = 1
			Else If @PaymentMode = 2
				Set @AmountCalc = 3
			Else If @PaymentMode = 3
				Set @AmountCalc = 6
			Else If @PaymentMode = 4
				Set @AmountCalc = 12
		
			If Convert(Datetime, @PayDueDate) <= Convert(Datetime, @ToDate)
				Begin
					Exec [usp_LeasePayment] @LeasePaymentId = 0, @CompanyId = @CompanyId, @LeaseSignUpId = @LeaseSignUpId, @TenantId = @TenantId, @DueDate = @PayDueDate, @PaymentStatus = 0, @Comment = '''', @InvoiceDate = '''', @PaymentDate = '''', @Op = ''I'', @ReturnValue = @ReturnValue Output
					
					If @ReturnValue <> 0
					Begin
						Declare curLSC cursor Local for    
						Select ChargeId, Mode, Amount From LeaseSignUp_Charge Where LeaseSignUpId = @LeaseSignUpId
						   
						open curLSC    
						Fetch next From curLSC Into @ChargeId, @Mode, @Amount
						while @@Fetch_Status = 0    
						Begin    
							If @Mode = ''Once''
								Begin
									If @ChargeOnce = 0
									Begin
										Set @ChargeAmount = (Convert(Numeric(18, 2), @Amount) * Convert(Numeric(18, 2), @TenantPercentage)) / Convert(Numeric(18, 2), 100)
										Exec [usp_LeasePayment_Charge] @LeasePayment_ChargeId = 0, @CompanyId = @CompanyId, @LeasePaymentId = @ReturnValue, @LeaseSignUpId = @LeaseSignUpId, @ChargeId = @ChargeId, @Amount = @ChargeAmount, @Op = ''I'', @ReturnValue = 0
										Set @ChargeOnce = 1
									End
								End
							Else If @Mode = ''Monthly''
								Begin           
									Set @ChargeAmount = @AmountCalc * (Convert(Numeric(18, 2), @Amount) * Convert(Numeric(18, 2), @TenantPercentage)) / Convert(Numeric(18, 2), 100)
									Exec [usp_LeasePayment_Charge] @LeasePayment_ChargeId = 0, @CompanyId = @CompanyId, @LeasePaymentId = @ReturnValue, @LeaseSignUpId = @LeaseSignUpId, @ChargeId = @ChargeId, @Amount = @ChargeAmount, @Op = ''I'', @ReturnValue = 0
								End
								
							Fetch next From curLSC Into @ChargeId, @Mode, @Amount
						End    
						Close curLSC    
						Deallocate curLSC
					End			
				End
			
			If @PaymentMode = 0 Or @PaymentMode = 1
				Set @PayDueDate = DateAdd(m, 1, @PayDueDate)
			Else If @PaymentMode = 2
				Set @PayDueDate = DateAdd(m, 3, @PayDueDate)
			Else If @PaymentMode = 3
				Set @PayDueDate = DateAdd(m, 6, @PayDueDate)
			Else If @PaymentMode = 4
				Set @PayDueDate = DateAdd(m, 12, @PayDueDate)

		End		
			
		Fetch next From curLST Into @TenantId, @TenantPercentage, @PaymentMode
	End    
	Close curLST    
	Deallocate curLST
End' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Lawyer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LeaseSignUp_Lawyer] 
	@LeaseSignUp_LawyerId As Int = 0,
	@CompanyId As Int = 0,
	@LeaseSignUpId As Int = 0,
    @PaymentDate As Varchar(Max) = Null, 
	@LawyerPercentage As Numeric(18, 2) = 0,
	@Op As Varchar(Max),
	@ReturnValue As Int=0 Output
AS
Begin
	If @Op = ''I''
		Begin
			Insert Into LeaseSignUp_Lawyer (LeaseSignUpId, PaymentDate, LawyerPercentage, CompanyId)
			Values (@LeaseSignUpId, @PaymentDate, @LawyerPercentage, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	Else If @Op = ''U''
		Begin
			Update LeaseSignUp_Lawyer Set LeaseSignUpId = @LeaseSignUpId, PaymentDate = @PaymentDate, LawyerPercentage = @LawyerPercentage
			Where LeaseSignUp_LawyerId = @LeaseSignUp_LawyerId
				
			Set @ReturnValue = @LeaseSignUp_LawyerId
		End
	Else If @Op = ''D''
		Begin
			Delete From LeaseSignUp_Lawyer Where LeaseSignUp_LawyerId = @LeaseSignUp_LawyerId
			Set @ReturnValue = @LeaseSignUp_LawyerId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeaseSignUp_LawyerGet Where LeaseSignUp_LawyerId = @LeaseSignUp_LawyerId  
		End	
End



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[usp_LeaseSignUp_Broker]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure [usp_LeaseSignUp_Broker] 
	@LeaseSignUp_BrokerId As Int = 0,
	@CompanyId As Int = 0,
	@LeaseSignUpId As Int = 0,
    @PaymentDate As Varchar(Max) = Null, 
	@BrokerPercentage As Numeric(18, 2) = 0,
	@Op As Varchar(Max),
	@ReturnValue As Int=0 Output
AS
Begin
	If @Op = ''I''
		Begin
			Insert Into LeaseSignUp_Broker (LeaseSignUpId, PaymentDate, BrokerPercentage, CompanyId)
			Values (@LeaseSignUpId, @PaymentDate, @BrokerPercentage, @CompanyId)
				
			Set @ReturnValue = Scope_Identity()
		End
	Else If @Op = ''U''
		Begin
			Update LeaseSignUp_Broker Set LeaseSignUpId = @LeaseSignUpId, PaymentDate = @PaymentDate, BrokerPercentage = @BrokerPercentage
			Where LeaseSignUp_BrokerId = @LeaseSignUp_BrokerId
				
			Set @ReturnValue = @LeaseSignUp_BrokerId
		End
	Else If @Op = ''D''
		Begin
			Delete From LeaseSignUp_Broker Where LeaseSignUp_BrokerId = @LeaseSignUp_BrokerId
			Set @ReturnValue = @LeaseSignUp_BrokerId
		End
	Else If @Op = ''G''
		Begin
			Select * From vwLeaseSignUp_BrokerGet Where LeaseSignUp_BrokerId = @LeaseSignUp_BrokerId  
		End	
End


' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_IssueRecord]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation]  WITH CHECK ADD  CONSTRAINT [FK_IssueAllocation_IssueRecord] FOREIGN KEY([IssueRecordId])
REFERENCES [IssueRecord] ([IssueRecordId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_IssueRecord]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] CHECK CONSTRAINT [FK_IssueAllocation_IssueRecord]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_MaintenanceStatus]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation]  WITH CHECK ADD  CONSTRAINT [FK_IssueAllocation_MaintenanceStatus] FOREIGN KEY([StatusId])
REFERENCES [MaintenanceStatus] ([MaintenanceStatusId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_IssueAllocation_MaintenanceStatus]') AND parent_object_id = OBJECT_ID(N'[IssueAllocation]'))
ALTER TABLE [IssueAllocation] CHECK CONSTRAINT [FK_IssueAllocation_MaintenanceStatus]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp]  WITH CHECK ADD  CONSTRAINT [FK_LeaseSignUp_Broker] FOREIGN KEY([BrokerId])
REFERENCES [Broker] ([BrokerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] CHECK CONSTRAINT [FK_LeaseSignUp_Broker]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp]  WITH CHECK ADD  CONSTRAINT [FK_LeaseSignUp_Lawyer] FOREIGN KEY([LawyerId])
REFERENCES [Lawyer] ([LawyerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp]'))
ALTER TABLE [LeaseSignUp] CHECK CONSTRAINT [FK_LeaseSignUp_Lawyer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Broker]'))
ALTER TABLE [LeaseSignUp_Broker]  WITH CHECK ADD  CONSTRAINT [FK_LeaseSignUp_Broker_LeaseSignUp] FOREIGN KEY([LeaseSignUpId])
REFERENCES [LeaseSignUp] ([LeaseSignUpId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Broker_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Broker]'))
ALTER TABLE [LeaseSignUp_Broker] CHECK CONSTRAINT [FK_LeaseSignUp_Broker_LeaseSignUp]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]'))
ALTER TABLE [LeaseSignUp_Lawyer]  WITH CHECK ADD  CONSTRAINT [FK_LeaseSignUp_Lawyer_LeaseSignUp] FOREIGN KEY([LeaseSignUpId])
REFERENCES [LeaseSignUp] ([LeaseSignUpId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_LeaseSignUp_Lawyer_LeaseSignUp]') AND parent_object_id = OBJECT_ID(N'[LeaseSignUp_Lawyer]'))
ALTER TABLE [LeaseSignUp_Lawyer] CHECK CONSTRAINT [FK_LeaseSignUp_Lawyer_LeaseSignUp]
GO
