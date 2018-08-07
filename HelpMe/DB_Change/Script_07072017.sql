If Not Exists (Select 1 From sys.columns Where name = 'ColorCode' And Object_Name(Object_Id) = 'tblCategory')
Begin
    Alter TABLE [dbo].[tblCategory] Add [ColorCode] [nvarchar](Max) NULL
End
GO
If Not Exists (Select 1 From sys.columns Where name = 'IsFree' And Object_Name(Object_Id) = 'tblCategory')
Begin
    Alter TABLE [dbo].[tblCategory] Add [IsFree] int NOT NULL CONSTRAINT [DF_tblCategory_IsFree]  DEFAULT ((0))
End
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[uspCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [uspCategory]
GO

CREATE Procedure [dbo].[uspCategory]
	@CategoryId As BigInt = null,      
	@CategoryName As nvarchar(MAX) = null,	
	@Icon1 As nvarchar(MAX) = null,	
	@Icon2 As nvarchar(MAX) = null,	
	@CategoryPoints As Int = 0,
	@Search As nvarchar(Max) = Null,
	@ColorCode As nvarchar(Max) = Null,
	@IsFree As Int = 0,   
	@Return_Value As BigInt = 0 Output,
	@IsActive As Int = 1,   
	@OpType As nvarchar(Max)
As      
Begin      
	Begin Try
		Begin Tran
			If @OpType = 'I'      
				Begin      
					Insert Into tblCategory (CategoryName, Icon1, Icon2, CategoryPoints,ColorCode,IsFree) Values (@CategoryName, @Icon1, @Icon2, @CategoryPoints,@ColorCode,@IsFree)
					
					Set @Return_Value = Scope_Identity()      
					
					Select CategoryId, CategoryName, Icon1, Icon2, CategoryPoints, IsActive,ColorCode,IsFree, CreatedOn, EndDate 
					From tblCategory Where CategoryId = @Return_Value
				End      
			Else If @OpType = 'U'      
				Begin      
					Update tblCategory Set CategoryName = @CategoryName, Icon1 = @Icon1, Icon2 = @Icon2, CategoryPoints = @CategoryPoints ,ColorCode=@ColorCode,IsFree=@IsFree
					Where CategoryId = @CategoryId		
					
					Set @Return_Value = @CategoryId 

					Select CategoryId, CategoryName, Icon1, Icon2, CategoryPoints, IsActive, CreatedOn, EndDate 
					From tblCategory Where CategoryId = @CategoryId
				End      
			Else If @OpType = 'S'
				Begin      
					Select CategoryId, CategoryName, Icon1, Icon2, CategoryPoints, IsActive,ColorCode,IsFree, CreatedOn, EndDate 
					From tblCategory Where CategoryId = @CategoryId
				End       
			Else If @OpType = 'Get'
				Begin      
					Select CategoryId, CategoryName, Icon1, Icon2, CategoryPoints,ColorCode,IsFree From tblCategory 
					Where IsActive = 1 And EndDate Is Null Order By CategoryName Asc
				End       
			Else If @OpType = 'IsExists'      
				Begin      
					Declare @IsExists As Int
					Set @IsExists = 0      

					If Exists (Select 1 From tblCategory Where CategoryId <> @CategoryId and CategoryName = @CategoryName And EndDate Is Null )
					Begin      
						Set @IsExists = 1      
					End         
					Select @IsExists As IsExists        
				End	
			Else If @OpType = 'GD'
				Begin
					Exec ('Select CategoryId As [Value], [Category Name] As [Text] From vwCategoryGet Where IsActive = 1' +  @Search + ' Order By [Category Name] Asc')
				End
		Commit Tran
	End Try
	Begin Catch
		Rollback Tran
		Insert Into PrjErrors (FormName, FunctionName, FilePath, ErrorMessage, ErrorDate, IsSent, ErrorSource, ErrorStack, ErrorInnerEx, ErrorData)
		Values (ERROR_PROCEDURE(), ERROR_PROCEDURE(), '', ERROR_MESSAGE(), GetDate(), ERROR_STATE(), ERROR_NUMBER(), ERROR_SEVERITY(), ERROR_LINE(), ERROR_MESSAGE())
		
		Declare @Error As nVarchar(Max) = Convert(nVarchar(Max), ERROR_NUMBER()) + '|' + ERROR_MESSAGE()
		Raiserror(@Error, 14, 2)	
	End Catch
End

GO
If Not Exists (Select 1 From sys.tables Where name = 'tblGeneralSettings')
 Begin
 
CREATE TABLE [dbo].[tblGeneralSettings](
	[GeneralSettingId] [bigint] IDENTITY(1,1) NOT NULL,
	[CreditPoint] [numeric](18, 0) NULL,
	[ShareApp] [numeric](18, 0) NULL,
	[SharePost] [numeric](18, 0) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_tblGeneralSettings] PRIMARY KEY CLUSTERED 
([GeneralSettingId] ASC)) ON [PRIMARY]
end

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[uspGeneralSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [uspGeneralSettings]
GO
CREATE PROCEDURE [dbo].[uspGeneralSettings]
	@GeneralSettingId As BigInt = null,      
	@CreditPoint as numeric(18,0) = null,
	@ShareApp as numeric(18,0) = null,
	@SharePost as numeric(18,0) = null,
	@Search As nvarchar(Max) = Null,
	@Return_Value As BigInt = 0 Output,
	@OpType As nvarchar(Max)	
AS
Begin      
	Begin Try
		Begin Tran
			If @OpType = 'I'      
				Begin      
					Insert Into tblGeneralSettings ( CreditPoint, ShareApp, SharePost, CreatedOn) 
					Values (@CreditPoint, @ShareApp, @SharePost,GETDATE())
					
					Set @Return_Value = Scope_Identity()      
					
					Select GeneralSettingId, CreditPoint, ShareApp, SharePost, CreatedOn, EndDate 
					From tblGeneralSettings Where GeneralSettingId = @Return_Value
				End      
			Else If @OpType = 'U'      
				Begin      
					Update tblGeneralSettings Set CreditPoint=@CreditPoint, ShareApp=@ShareApp, SharePost=@SharePost
					Where GeneralSettingId = @GeneralSettingId		
					
					Set @Return_Value = @GeneralSettingId 

					Select GeneralSettingId, CreditPoint, ShareApp, SharePost, CreatedOn, EndDate 
					From tblGeneralSettings Where GeneralSettingId = @GeneralSettingId
				End      
			Else If @OpType = 'S'
				Begin      
					Select GeneralSettingId, CreditPoint, ShareApp, SharePost, CreatedOn, EndDate 
					From tblGeneralSettings Where GeneralSettingId = @GeneralSettingId
				End       
			Else If @OpType = 'Get'
				Begin      
					Select GeneralSettingId, CreditPoint, ShareApp, SharePost, CreatedOn, EndDate  From tblGeneralSettings 
					Where EndDate Is Null Order By CreditPoint Asc
				End       
			Else If @OpType = 'IsExists'      
				Begin      
					Declare @IsExists As Int
					Set @IsExists = 0      

					If Exists (Select 1 From tblGeneralSettings Where GeneralSettingId <> @GeneralSettingId and CreditPoint = @CreditPoint And EndDate Is Null )
					Begin      
						Set @IsExists = 1      
					End         
					Select @IsExists As IsExists        
				End	
			--Else If @OpType = 'GD'
			--	Begin
			--		Exec ('Select GeneralSettingId As [Value], [CreditPoint] As [Text] From vwPackageGet Where ' +  @Search + ' Order By [PackageName] Asc')
			--	End
		Commit Tran
	End Try
	Begin Catch
		Rollback Tran
		Insert Into PrjErrors (FormName, FunctionName, FilePath, ErrorMessage, ErrorDate, IsSent, ErrorSource, ErrorStack, ErrorInnerEx, ErrorData)
		Values (ERROR_PROCEDURE(), ERROR_PROCEDURE(), '', ERROR_MESSAGE(), GetDate(), ERROR_STATE(), ERROR_NUMBER(), ERROR_SEVERITY(), ERROR_LINE(), ERROR_MESSAGE())
		
		Declare @Error As nVarchar(Max) = Convert(nVarchar(Max), ERROR_NUMBER()) + '|' + ERROR_MESSAGE()
		Raiserror(@Error, 14, 2)	
	End Catch
End

GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[vwGeneralSettings]'))
DROP VIEW [vwGeneralSettings]
GO
CREATE View [dbo].[vwGeneralSettings] As
Select GeneralSettingId, CreditPoint, ShareApp, SharePost, CreatedOn, EndDate
From tblGeneralSettings where enddate is null
Go
If Not Exists (Select 1 From sys.columns Where name = 'CreditPoint' And Object_Name(Object_Id) = 'tblClient')
Begin
   Alter TABLE [dbo].[tblClient] Add [CreditPoint] int
End
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[uspClient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [uspClient]
GO
CREATE Procedure [dbo].[uspClient]
	@ClientId As BigInt = null,      
	@FirstName As nvarchar(MAX) = null,	
	@LastName As nvarchar(MAX) = null,	
	@Gender As Int = null,
	@CreditPoint As Int = null,
	@Address1 As nvarchar(MAX) = null,	
	@Address2 As nvarchar(MAX) = null,	
	@City As BigInt = null,	
	@POBox As nvarchar(MAX) = null,	
	@State As BigInt = null,	
	@Country As BigInt = null,	
	@PhoneNo As nvarchar(MAX) = null,	
	@EmailId As nvarchar(MAX) = null,	
	@Password As nvarchar(MAX) = null,	
	@ProfilePic As varchar(MAX) = null,	
	@AcTokenId As varchar(MAX) = null,	
	@RegisteredBy As varchar(MAX) = '0',
	@Return_Value As BigInt = 0 Output,
	@IsActive As Int = 1,   
	@OpType As nvarchar(Max)
As      
Begin      
	Begin Try
		Begin Tran
			If @OpType = 'I'      
				Begin      
					Insert Into tblClient (FirstName, LastName, Gender, EmailId, Password, AcTokenId, RegisteredBy,CreditPoint)
					Values (@FirstName, @LastName, 0, @EmailId, @Password, @AcTokenId, @RegisteredBy,@CreditPoint)

					Set @Return_Value = Scope_Identity()      
					
					Select CMM.ClientId, CMM.FirstName, CMM.LastName, ISNULL(CMM.Gender, 0) As Gender, Case ISNULL(CMM.Gender, 0) When 0 Then '' When 1 Then 'Male' When 2 Then 'Female' End As GenderDisp,
						ISNULL(CMM.Address1, '') AS Address1, ISNULL(CMM.Address2, '') AS Address2, ISNULL(CMM.City, 0) AS City, ISNULL(CMM.POBox, '') AS POBox, 
						ISNULL(CMM.State, 0) AS State, ISNULL(CMM.Country, 0) AS Country, ISNULL(CMM.PhoneNo, '') AS PhoneNo, ISNULL(CMM.EmailId, '') AS EmailId, CMM.Password, 
						CASE CMM.IsActive WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, CMM.IsActive, CMM.CreatedOn, CMM.EndDate, CMM.ProfilePic, CMM.AcTokenId, Isnull(CMM.RegisteredBy, 0) As RegisteredBy, 
						Isnull(C.CityName, '') As CityName, Isnull(S.StateName, '') As StateName, Isnull(CM.CountryName, '') As CountryName, CMM.Latitude, CMM.Longitude,CMM.CreditPoint, CMM.Altitude,
						Isnull(CMM.ProfilePic, '') As ProfilePic, Isnull(CMM.IsClientProfile, 0) As IsClientProfile
					From dbo.tblClient CMM
						Left Outer Join tblCity C On CMM.City = C.CityId
						Left Outer Join tblState S On CMM.State = S.StateId
						Left Outer Join tblCountry CM On CMM.Country = CM.CountryId
					Where CMM.EndDate Is Null And CMM.ClientId = @Return_Value
				End      
			Else If @OpType = 'U'      
				Begin      
					Update tblClient Set 
						FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Address1 = @Address1, Address2 = @Address2, City = @City, POBox = @POBox, CreditPoint=@CreditPoint,
						State = @State, Country = @Country, PhoneNo = @PhoneNo, EmailId = @EmailId, IsClientProfile = 1
					Where ClientId = @ClientId
			
					Set @Return_Value = @ClientId 
					
					Select CMM.ClientId, CMM.FirstName, CMM.LastName, ISNULL(CMM.Gender, 0) As Gender, Case ISNULL(CMM.Gender, 0) When 0 Then '' When 1 Then 'Male' When 2 Then 'Female' End As GenderDisp,
						ISNULL(CMM.Address1, '') AS Address1, ISNULL(CMM.Address2, '') AS Address2, ISNULL(CMM.City, 0) AS City, ISNULL(CMM.POBox, '') AS POBox, 
						ISNULL(CMM.State, 0) AS State, ISNULL(CMM.Country, 0) AS Country, ISNULL(CMM.PhoneNo, '') AS PhoneNo, ISNULL(CMM.EmailId, '') AS EmailId, CMM.Password, 
						CASE CMM.IsActive WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, CMM.IsActive, CMM.CreatedOn, CMM.EndDate, CMM.ProfilePic, CMM.AcTokenId, Isnull(CMM.RegisteredBy, 0) As RegisteredBy, 
						Isnull(C.CityName, '') As CityName, Isnull(S.StateName, '') As StateName, Isnull(CM.CountryName, '') As CountryName, CMM.Latitude, CMM.Longitude, CMM.Altitude,CMM.CreditPoint,
						Isnull(CMM.ProfilePic, '') As ProfilePic, Isnull(CMM.IsClientProfile, 0) As IsClientProfile
					From dbo.tblClient CMM
						Left Outer Join tblCity C On CMM.City = C.CityId
						Left Outer Join tblState S On CMM.State = S.StateId
						Left Outer Join tblCountry CM On CMM.Country = CM.CountryId
					Where CMM.EndDate Is Null And CMM.ClientId = @ClientId
				End	      
			Else If @OpType = 'CP'      
				Begin      
					Update tblClient Set Password = @Password Where ClientId = @ClientId		
					
					Select CMM.ClientId, CMM.FirstName, CMM.LastName, ISNULL(CMM.Gender, 0) As Gender, Case ISNULL(CMM.Gender, 0) When 0 Then '' When 1 Then 'Male' When 2 Then 'Female' End As GenderDisp, 
						ISNULL(CMM.Address1, '') AS Address1, ISNULL(CMM.Address2, '') AS Address2, ISNULL(CMM.City, 0) AS City, ISNULL(CMM.POBox, '') AS POBox, 
						ISNULL(CMM.State, 0) AS State, ISNULL(CMM.Country, 0) AS Country, ISNULL(CMM.PhoneNo, '') AS PhoneNo, ISNULL(CMM.EmailId, '') AS EmailId, CMM.Password, 
						CASE CMM.IsActive WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, CMM.IsActive, CMM.CreatedOn, CMM.EndDate, CMM.ProfilePic, CMM.AcTokenId, Isnull(CMM.RegisteredBy, 0) As RegisteredBy, 
						Isnull(C.CityName, '') As CityName, Isnull(S.StateName, '') As StateName, Isnull(CM.CountryName, '') As CountryName, CMM.Latitude, CMM.Longitude, CMM.Altitude,CMM.CreditPoint,
						Isnull(CMM.ProfilePic, '') As ProfilePic, Isnull(CMM.IsClientProfile, 0) As IsClientProfile
					From dbo.tblClient CMM
						Left Outer Join tblCity C On CMM.City = C.CityId
						Left Outer Join tblState S On CMM.State = S.StateId
						Left Outer Join tblCountry CM On CMM.Country = CM.CountryId
					Where CMM.EndDate Is Null And CMM.ClientId = @ClientId
				End       
			Else If @OpType = 'PP'      
				Begin      
					Update tblClient Set ProfilePic = @ProfilePic Where ClientId = @ClientId		
					
					Select CMM.ClientId, CMM.FirstName, CMM.LastName, ISNULL(CMM.Gender, 0) As Gender, Case ISNULL(CMM.Gender, 0) When 0 Then '' When 1 Then 'Male' When 2 Then 'Female' End As GenderDisp, 
						ISNULL(CMM.Address1, '') AS Address1, ISNULL(CMM.Address2, '') AS Address2, ISNULL(CMM.City, 0) AS City, ISNULL(CMM.POBox, '') AS POBox, 
						ISNULL(CMM.State, 0) AS State, ISNULL(CMM.Country, 0) AS Country, ISNULL(CMM.PhoneNo, '') AS PhoneNo, ISNULL(CMM.EmailId, '') AS EmailId, CMM.Password, 
						CASE CMM.IsActive WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, CMM.IsActive, CMM.CreatedOn, CMM.EndDate, CMM.ProfilePic, CMM.AcTokenId, Isnull(CMM.RegisteredBy, 0) As RegisteredBy, 
						Isnull(C.CityName, '') As CityName, Isnull(S.StateName, '') As StateName, Isnull(CM.CountryName, '') As CountryName, CMM.Latitude, CMM.Longitude, CMM.Altitude,CMM.CreditPoint,
						Isnull(CMM.ProfilePic, '') As ProfilePic, Isnull(CMM.IsClientProfile, 0) As IsClientProfile
					From dbo.tblClient CMM
						Left Outer Join tblCity C On CMM.City = C.CityId
						Left Outer Join tblState S On CMM.State = S.StateId
						Left Outer Join tblCountry CM On CMM.Country = CM.CountryId
					Where CMM.EndDate Is Null And CMM.ClientId = @ClientId
				End       
			Else If @OpType = 'S'      
				Begin      
					Select CMM.ClientId, CMM.FirstName, CMM.LastName, ISNULL(CMM.Gender, 0) As Gender, Case ISNULL(CMM.Gender, 0) When 0 Then '' When 1 Then 'Male' When 2 Then 'Female' End As GenderDisp, 
						ISNULL(CMM.Address1, '') AS Address1, ISNULL(CMM.Address2, '') AS Address2, ISNULL(CMM.City, 0) AS City, ISNULL(CMM.POBox, '') AS POBox, 
						ISNULL(CMM.State, 0) AS State, ISNULL(CMM.Country, 0) AS Country, ISNULL(CMM.PhoneNo, '') AS PhoneNo, ISNULL(CMM.EmailId, '') AS EmailId, CMM.Password, 
						CASE CMM.IsActive WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END AS Status, CMM.IsActive, CMM.CreatedOn, CMM.EndDate, CMM.ProfilePic, CMM.AcTokenId, Isnull(CMM.RegisteredBy, 0) As RegisteredBy, 
						Isnull(C.CityName, '') As CityName, Isnull(S.StateName, '') As StateName, Isnull(CM.CountryName, '') As CountryName, CMM.Latitude, CMM.Longitude, CMM.Altitude,CMM.CreditPoint,
						Isnull(CMM.ProfilePic, '') As ProfilePic, Isnull(CMM.IsClientProfile, 0) As IsClientProfile
					From dbo.tblClient CMM
						Left Outer Join tblCity C On CMM.City = C.CityId
						Left Outer Join tblState S On CMM.State = S.StateId
						Left Outer Join tblCountry CM On CMM.Country = CM.CountryId
					Where CMM.EndDate Is Null And CMM.ClientId = @ClientId
				End       
			Else If @OpType = 'IsExists'      
				Begin      
					Declare @IsExists As Int
					Set @IsExists = 0      

					If Exists (Select 1 From tblClient Where EmailId = @EmailId)      
					Begin      
						Set @IsExists = 1      
					End         
					Select @IsExists As IsExists        
				End
		Commit Tran
	End Try
	Begin Catch
		Rollback Tran
		Insert Into PrjErrors (FormName, FunctionName, FilePath, ErrorMessage, ErrorDate, IsSent, ErrorSource, ErrorStack, ErrorInnerEx, ErrorData)
		Values (ERROR_PROCEDURE(), ERROR_PROCEDURE(), '', ERROR_MESSAGE(), GetDate(), ERROR_STATE(), ERROR_NUMBER(), ERROR_SEVERITY(), ERROR_LINE(), ERROR_MESSAGE())
		
		Declare @Error As nVarchar(Max) = Convert(nVarchar(Max), ERROR_NUMBER()) + '|' + ERROR_MESSAGE()
		Raiserror(@Error, 14, 2)	
	End Catch
End



