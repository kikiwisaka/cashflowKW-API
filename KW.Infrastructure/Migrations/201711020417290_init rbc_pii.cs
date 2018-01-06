namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initrbc_pii : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblACAs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contractType = c.Int(nullable: false),
                        measurementStart = c.String(),
                        measurementEnd = c.String(),
                        administrativeStart = c.String(),
                        administrativeEnd = c.String(),
                        stabilityStart = c.String(),
                        stabilityEnd = c.String(),
                        contractDate = c.String(),
                        contractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblJobContracts", t => t.contractId)
                .Index(t => t.contractId);
            
            CreateTable(
                "dbo.tblJobContracts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contractType = c.Int(nullable: false),
                        startDate = c.String(),
                        endDate = c.String(),
                        payRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayType = c.Int(nullable: false),
                        payTypeName = c.String(),
                        isActive = c.Int(nullable: false),
                        terminateReason = c.Int(nullable: false),
                        employeeId = c.Int(nullable: false),
                        employeeTypes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployeeTypess", t => t.employeeTypes)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .Index(t => t.employeeId)
                .Index(t => t.employeeTypes);
            
            CreateTable(
                "dbo.tblEmployee",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        salutation = c.Int(nullable: false),
                        firstName = c.String(),
                        middleName = c.String(),
                        lastName = c.String(),
                        ssn = c.String(),
                        dob = c.String(),
                        cellNumber = c.String(),
                        phoneNumber = c.String(),
                        genderId = c.Int(nullable: false),
                        primaryLangId = c.String(),
                        futherName1 = c.String(),
                        futherName2 = c.String(),
                        futherName3 = c.String(),
                        isSenior = c.Boolean(nullable: false),
                        street = c.String(),
                        street2 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        countryId = c.String(),
                        payrollId = c.String(),
                        hireDate = c.String(),
                        modifyBy = c.Int(nullable: false),
                        modifyDate = c.String(),
                        modifyTime = c.String(),
                        lastStep = c.Int(nullable: false),
                        isWizard = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblGenders", t => t.genderId)
                .ForeignKey("dbo.tblSalutations", t => t.salutation)
                .Index(t => t.salutation)
                .Index(t => t.genderId);
            
            CreateTable(
                "dbo.tblBenefitEligibles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        employeeId = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        month = c.Int(nullable: false),
                        isEligible = c.Boolean(nullable: false),
                        createBy = c.String(),
                        createDate = c.String(),
                        createTime = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .Index(t => t.employeeId);
            
            CreateTable(
                "dbo.tblEmergencyContact",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contactName = c.String(),
                        address = c.String(),
                        languageId = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        countryId = c.String(),
                        relationship = c.Int(nullable: false),
                        phoneNumber = c.String(),
                        employeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .Index(t => t.employeeId);
            
            CreateTable(
                "dbo.tblEmployeeCompliance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        expiredDate = c.String(),
                        information = c.String(),
                        isActive = c.Boolean(nullable: false),
                        insertBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        empId = c.Int(nullable: false),
                        compTrackingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblEmployee", t => t.empId)
                .ForeignKey("dbo.tblEmployeeComplianceTrackings", t => t.compTrackingId)
                .Index(t => t.empId)
                .Index(t => t.compTrackingId);
            
            CreateTable(
                "dbo.tblDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        expDate = c.String(),
                        fileName = c.String(),
                        saveFileName = c.Guid(nullable: false),
                        saveFilePath = c.String(),
                        fileType = c.String(),
                        uploadBy = c.Int(nullable: false),
                        uploadDate = c.String(),
                        uploadTime = c.String(),
                        empComplianceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblEmployeeCompliance", t => t.empComplianceId)
                .Index(t => t.empComplianceId);
            
            CreateTable(
                "dbo.tblEmployeeComplianceTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        alias = c.String(),
                        complianceValue = c.Boolean(nullable: false),
                        hasExpiryDate = c.Boolean(nullable: false),
                        mandatoryExpiryDate = c.Boolean(nullable: false),
                        hasAttachment = c.Boolean(nullable: false),
                        mandatoryHasAttachment = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        complianceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCompliances", t => t.complianceId)
                .Index(t => t.complianceId);
            
            CreateTable(
                "dbo.tblCompliances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        hasExpiryDate = c.Boolean(nullable: false),
                        mandatoryExpiryDate = c.Boolean(nullable: false),
                        hasAttachment = c.Boolean(nullable: false),
                        mandatoryHasAttachment = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblEmployeePaidWages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        year = c.Int(nullable: false),
                        month = c.Int(nullable: false),
                        totalHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalWages = c.Decimal(nullable: false, precision: 18, scale: 2),
                        employeeId = c.Int(nullable: false),
                        payrollRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .ForeignKey("dbo.tblPayrollRecords", t => t.payrollRecordId)
                .Index(t => t.employeeId)
                .Index(t => t.payrollRecordId);
            
            CreateTable(
                "dbo.tblPayrollRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        payrollId = c.Int(nullable: false),
                        ssn = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        zipCode = c.String(),
                        employeeType = c.Int(nullable: false),
                        hireDate = c.String(),
                        inactiveDate = c.String(),
                        gender = c.String(),
                        birthDate = c.String(),
                        totalHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalWages = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isSsn = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        comment = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        payrollImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblImportPayroll", t => t.payrollImportId)
                .Index(t => t.payrollImportId);
            
            CreateTable(
                "dbo.tblImportPayroll",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fileName = c.String(),
                        saveFileName = c.String(),
                        saveFilePath = c.String(),
                        fileExtension = c.String(),
                        month = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        importBy = c.Int(nullable: false),
                        importDate = c.String(),
                        importTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblBenefitEnrollment",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rateType = c.String(),
                        offerDate = c.String(),
                        startDate = c.String(),
                        endDate = c.String(),
                        declineReason = c.Int(nullable: false),
                        createBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        remark = c.String(),
                        status = c.Int(nullable: false),
                        isCobraEvent = c.Boolean(nullable: false),
                        isOversight = c.Boolean(nullable: false),
                        employeeId = c.Int(nullable: false),
                        benefitPlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblBenefitPlans", t => t.benefitPlanId)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .Index(t => t.employeeId)
                .Index(t => t.benefitPlanId);
            
            CreateTable(
                "dbo.tblBenefitPlans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        document = c.Guid(nullable: false),
                        name = c.String(),
                        startDate = c.String(),
                        endDate = c.String(),
                        rateEmpOnly = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rateEmpSpouse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rateEmpChildren = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rateEmpFamily = c.Decimal(nullable: false, precision: 18, scale: 2),
                        empOnlyShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        empSpouseShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        empChildShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        empFamilyShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        deductableAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        coinsurance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        physOfficeCopayAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        pharmacyCopayAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        status = c.Int(nullable: false),
                        isBasePlan = c.Boolean(nullable: false),
                        isMinValue = c.Boolean(nullable: false),
                        insuredType = c.Int(nullable: false),
                        createBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        deleteBy = c.Int(nullable: false),
                        deleteDate = c.String(),
                        deleteTime = c.String(),
                        insCompanyId = c.Int(nullable: false),
                        benefitTypeId = c.Int(nullable: false),
                        benefitPlanYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblBenefitPlanYears", t => t.benefitPlanYearId)
                .ForeignKey("dbo.tblBenefitTypes", t => t.benefitTypeId)
                .ForeignKey("dbo.tblInsuranceCompanys", t => t.insCompanyId)
                .Index(t => t.insCompanyId)
                .Index(t => t.benefitTypeId)
                .Index(t => t.benefitPlanYearId);
            
            CreateTable(
                "dbo.tblBenefitPlanYears",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        year = c.String(),
                        startDate = c.String(),
                        endDate = c.String(),
                        trackingIndicator = c.Int(nullable: false),
                        waitingPeriod = c.Int(nullable: false),
                        varHoursEmployeeNewHiremeasurementPeriod = c.String(),
                        varHoursEmployeeNewHiremeasurementLengthPeriod = c.Int(nullable: false),
                        nonVarHoursEmployeeNewHiremeasurementPeriod = c.String(),
                        nonVarHoursEmployeeNewHiremeasurementLengthPeriod = c.Int(nullable: false),
                        standarMeasurementStartDate = c.String(),
                        standarMeasurementLength = c.Int(nullable: false),
                        standarmeasurementlengthperiod = c.String(),
                        administrativelength = c.Int(nullable: false),
                        administrativesLengthPeriod = c.String(),
                        administrativeStartDate = c.String(),
                        stabilityStartDate = c.String(),
                        stabilityLength = c.Int(nullable: false),
                        stabilityLengthPeriod = c.String(),
                        insertBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblPlanYearEmployeeTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BenefitPlanYearId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployeeTypess", t => t.EmployeeTypeId)
                .ForeignKey("dbo.tblBenefitPlanYears", t => t.BenefitPlanYearId)
                .Index(t => t.BenefitPlanYearId)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.tblEmployeeTypess",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        employeeeTypes = c.String(),
                        eligibleVaraibleHour = c.Int(nullable: false),
                        numberOfPayPeriodPerYear = c.Double(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        updateBy = c.String(),
                        insertBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblRoleEmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblEmployeeTypess", t => t.EmployeeTypeId)
                .ForeignKey("dbo.tblRole", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.tblRole",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        createDate = c.String(),
                        createTime = c.String(),
                        status = c.Boolean(nullable: false),
                        isMasking = c.Boolean(nullable: false),
                        isAllJobs = c.Boolean(nullable: false),
                        creatorUserId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblUsers", t => t.creatorUserId)
                .Index(t => t.creatorUserId);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        password = c.String(),
                        status = c.Boolean(nullable: false),
                        language = c.String(),
                        createDate = c.String(),
                        createTime = c.String(),
                        expirydate = c.String(),
                        lastupdatedate = c.String(),
                        employeeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .Index(t => t.employeeId);
            
            CreateTable(
                "dbo.tblMaskingFieldsSetting",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fieldName = c.String(),
                        alias = c.String(),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        insertBy = c.Int(nullable: false),
                        isMasking = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DueDate = c.String(),
                        Priority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        Description = c.String(),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.String(),
                        CreateTime = c.String(),
                        UpdateBy = c.Int(nullable: false),
                        UpdateDate = c.String(),
                        UpdateTime = c.String(),
                        AssignTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblUsers", t => t.AssignTo)
                .Index(t => t.AssignTo);
            
            CreateTable(
                "dbo.UserTaskRegardings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserTaskId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.EmployeeId)
                .ForeignKey("dbo.UserTasks", t => t.UserTaskId)
                .Index(t => t.UserTaskId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tblUserRole",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.tblRole", t => t.id)
                .ForeignKey("dbo.tblUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.tblRoleAccess",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblMenu", t => t.MenuId)
                .ForeignKey("dbo.tblRole", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.tblMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        controllername = c.String(),
                        actionname = c.String(),
                        active = c.Boolean(nullable: false),
                        isgeneralaccess = c.Boolean(nullable: false),
                        isonmenu = c.Boolean(nullable: false),
                        styleclass = c.String(),
                        icon = c.String(),
                        sequence = c.Int(nullable: false),
                        isSuperAdmin = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblMenu", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.tblAPIMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        APIId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblAPI", t => t.APIId)
                .ForeignKey("dbo.tblMenu", t => t.MenuId)
                .Index(t => t.APIId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.tblAPI",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        controllername = c.String(),
                        actionname = c.String(),
                        fileType = c.Boolean(nullable: false),
                        isgeneralaccess = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblBenefitTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BenefitType = c.String(),
                        CreateDate = c.String(),
                        CreateTime = c.String(),
                        UpdateDate = c.String(),
                        UpdateTime = c.String(),
                        UpdateBy = c.String(),
                        InsertBy = c.String(),
                        IsMedical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblInsuranceCompanys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyName = c.String(),
                        phoneNumber = c.String(),
                        faxNumber = c.String(),
                        contactName = c.String(),
                        email = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipcode = c.String(),
                        address = c.String(),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        updateBy = c.String(),
                        insertBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblBenefitFamilyTrackingOffers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        remark = c.String(),
                        createBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        isActive = c.Boolean(nullable: false),
                        enrollId = c.Int(nullable: false),
                        familyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblBenefitEnrollment", t => t.enrollId)
                .ForeignKey("dbo.tblFamilys", t => t.familyId)
                .Index(t => t.enrollId)
                .Index(t => t.familyId);
            
            CreateTable(
                "dbo.tblFamilys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dateOfBirth = c.String(),
                        ssn = c.String(),
                        firstName = c.String(),
                        middleName = c.String(),
                        lastName = c.String(),
                        relationType = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        insertBy = c.Int(nullable: false),
                        employeeId = c.Int(nullable: false),
                        genderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.employeeId)
                .ForeignKey("dbo.tblGenders", t => t.genderId)
                .Index(t => t.employeeId)
                .Index(t => t.genderId);
            
            CreateTable(
                "dbo.tblGenders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        genderName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblSalutations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        saluteName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblWotcDocuments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        docType = c.Int(nullable: false),
                        fileName = c.String(),
                        saveFileName = c.Guid(nullable: false),
                        saveFilePath = c.String(),
                        fileType = c.Int(nullable: false),
                        insertBy = c.Int(nullable: false),
                        insertDate = c.String(),
                        insertTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                        approvalNote = c.String(),
                        isRead = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        wotcEligible = c.Int(nullable: false),
                        wotcTargetId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.EmployeeId)
                .ForeignKey("dbo.tblWotcTargets", t => t.wotcTargetId)
                .Index(t => t.wotcTargetId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.tblWotcTargets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        createBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblJobPayRates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        payRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isActive = c.Int(nullable: false),
                        description = c.String(),
                        jobContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblJobContracts", t => t.jobContractId)
                .Index(t => t.jobContractId);
            
            CreateTable(
                "dbo.tblCompanys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ein = c.String(),
                        companyName = c.String(),
                        phoneNumber = c.String(),
                        faxNumber = c.String(),
                        contactName = c.String(),
                        email = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        addressline1 = c.String(),
                        addressline2 = c.String(),
                        isWizardFinish = c.Int(nullable: false),
                        insertBy = c.Int(nullable: false),
                        createDate = c.String(),
                        createTime = c.String(),
                        updateBy = c.Int(nullable: false),
                        updateDate = c.String(),
                        updateTime = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblDeclineReasons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        reason = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        Degree = c.String(),
                        StudyField = c.String(),
                        Grade = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblEmployee", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.tblProjects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaProject = c.String(),
                        userProject = c.Int(nullable: false),
                        tahapanId = c.Int(nullable: false),
                        statusProject = c.Boolean(nullable: false),
                        minimum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        maximum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sektorId = c.Int(nullable: false),
                        keterangan = c.String(),
                        createBy = c.Int(),
                        createDate = c.DateTime(),
                        updateBy = c.Int(),
                        updateDate = c.DateTime(),
                        isDelete = c.Boolean(),
                        deleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblSektors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaSektor = c.String(),
                        minimum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        maximum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        definisi = c.String(),
                        createBy = c.Int(),
                        createDate = c.DateTime(),
                        updateBy = c.Int(),
                        updateDate = c.DateTime(),
                        isDelete = c.Boolean(),
                        deleteDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblStages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaStage = c.String(),
                        keterangan = c.String(),
                        createBy = c.Int(),
                        createDate = c.DateTime(),
                        updateBy = c.Int(),
                        updateDate = c.DateTime(),
                        isDelete = c.Boolean(),
                        deleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblTahapans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaTahapan = c.String(),
                        keterangan = c.String(),
                        createBy = c.Int(),
                        createDate = c.DateTime(),
                        updateBy = c.Int(),
                        updateDate = c.DateTime(),
                        isDelete = c.Boolean(),
                        deleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblTerminationReasons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        terminationName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblUsersResetPassword",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        requestToken = c.String(),
                        requestDate = c.String(),
                        timeRequest = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educations", "Employee_Id", "dbo.tblEmployee");
            DropForeignKey("dbo.tblACAs", "contractId", "dbo.tblJobContracts");
            DropForeignKey("dbo.tblJobPayRates", "jobContractId", "dbo.tblJobContracts");
            DropForeignKey("dbo.tblWotcDocuments", "wotcTargetId", "dbo.tblWotcTargets");
            DropForeignKey("dbo.tblWotcDocuments", "EmployeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblEmployee", "salutation", "dbo.tblSalutations");
            DropForeignKey("dbo.tblJobContracts", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblEmployee", "genderId", "dbo.tblGenders");
            DropForeignKey("dbo.tblBenefitFamilyTrackingOffers", "familyId", "dbo.tblFamilys");
            DropForeignKey("dbo.tblFamilys", "genderId", "dbo.tblGenders");
            DropForeignKey("dbo.tblFamilys", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblBenefitFamilyTrackingOffers", "enrollId", "dbo.tblBenefitEnrollment");
            DropForeignKey("dbo.tblBenefitEnrollment", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblBenefitPlans", "insCompanyId", "dbo.tblInsuranceCompanys");
            DropForeignKey("dbo.tblBenefitEnrollment", "benefitPlanId", "dbo.tblBenefitPlans");
            DropForeignKey("dbo.tblBenefitPlans", "benefitTypeId", "dbo.tblBenefitTypes");
            DropForeignKey("dbo.tblBenefitPlans", "benefitPlanYearId", "dbo.tblBenefitPlanYears");
            DropForeignKey("dbo.tblPlanYearEmployeeTypes", "BenefitPlanYearId", "dbo.tblBenefitPlanYears");
            DropForeignKey("dbo.tblRoleEmployeeTypes", "RoleId", "dbo.tblRole");
            DropForeignKey("dbo.tblRoleAccess", "RoleId", "dbo.tblRole");
            DropForeignKey("dbo.tblRoleAccess", "MenuId", "dbo.tblMenu");
            DropForeignKey("dbo.tblMenu", "ParentId", "dbo.tblMenu");
            DropForeignKey("dbo.tblAPIMenu", "MenuId", "dbo.tblMenu");
            DropForeignKey("dbo.tblAPIMenu", "APIId", "dbo.tblAPI");
            DropForeignKey("dbo.tblRole", "creatorUserId", "dbo.tblUsers");
            DropForeignKey("dbo.tblUserRole", "UserId", "dbo.tblUsers");
            DropForeignKey("dbo.tblUserRole", "id", "dbo.tblRole");
            DropForeignKey("dbo.UserTasks", "AssignTo", "dbo.tblUsers");
            DropForeignKey("dbo.UserTaskRegardings", "UserTaskId", "dbo.UserTasks");
            DropForeignKey("dbo.UserTaskRegardings", "EmployeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblMaskingFieldsSetting", "User_Id", "dbo.tblUsers");
            DropForeignKey("dbo.tblUsers", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblRoleEmployeeTypes", "EmployeeTypeId", "dbo.tblEmployeeTypess");
            DropForeignKey("dbo.tblPlanYearEmployeeTypes", "EmployeeTypeId", "dbo.tblEmployeeTypess");
            DropForeignKey("dbo.tblJobContracts", "employeeTypes", "dbo.tblEmployeeTypess");
            DropForeignKey("dbo.tblEmployeePaidWages", "payrollRecordId", "dbo.tblPayrollRecords");
            DropForeignKey("dbo.tblPayrollRecords", "payrollImportId", "dbo.tblImportPayroll");
            DropForeignKey("dbo.tblEmployeePaidWages", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblEmployeeCompliance", "compTrackingId", "dbo.tblEmployeeComplianceTrackings");
            DropForeignKey("dbo.tblEmployeeComplianceTrackings", "complianceId", "dbo.tblCompliances");
            DropForeignKey("dbo.tblEmployeeCompliance", "empId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblDocuments", "empComplianceId", "dbo.tblEmployeeCompliance");
            DropForeignKey("dbo.tblEmergencyContact", "employeeId", "dbo.tblEmployee");
            DropForeignKey("dbo.tblBenefitEligibles", "employeeId", "dbo.tblEmployee");
            DropIndex("dbo.Educations", new[] { "Employee_Id" });
            DropIndex("dbo.tblJobPayRates", new[] { "jobContractId" });
            DropIndex("dbo.tblWotcDocuments", new[] { "EmployeeId" });
            DropIndex("dbo.tblWotcDocuments", new[] { "wotcTargetId" });
            DropIndex("dbo.tblFamilys", new[] { "genderId" });
            DropIndex("dbo.tblFamilys", new[] { "employeeId" });
            DropIndex("dbo.tblBenefitFamilyTrackingOffers", new[] { "familyId" });
            DropIndex("dbo.tblBenefitFamilyTrackingOffers", new[] { "enrollId" });
            DropIndex("dbo.tblAPIMenu", new[] { "MenuId" });
            DropIndex("dbo.tblAPIMenu", new[] { "APIId" });
            DropIndex("dbo.tblMenu", new[] { "ParentId" });
            DropIndex("dbo.tblRoleAccess", new[] { "MenuId" });
            DropIndex("dbo.tblRoleAccess", new[] { "RoleId" });
            DropIndex("dbo.tblUserRole", new[] { "id" });
            DropIndex("dbo.tblUserRole", new[] { "UserId" });
            DropIndex("dbo.UserTaskRegardings", new[] { "EmployeeId" });
            DropIndex("dbo.UserTaskRegardings", new[] { "UserTaskId" });
            DropIndex("dbo.UserTasks", new[] { "AssignTo" });
            DropIndex("dbo.tblMaskingFieldsSetting", new[] { "User_Id" });
            DropIndex("dbo.tblUsers", new[] { "employeeId" });
            DropIndex("dbo.tblRole", new[] { "creatorUserId" });
            DropIndex("dbo.tblRoleEmployeeTypes", new[] { "EmployeeTypeId" });
            DropIndex("dbo.tblRoleEmployeeTypes", new[] { "RoleId" });
            DropIndex("dbo.tblPlanYearEmployeeTypes", new[] { "EmployeeTypeId" });
            DropIndex("dbo.tblPlanYearEmployeeTypes", new[] { "BenefitPlanYearId" });
            DropIndex("dbo.tblBenefitPlans", new[] { "benefitPlanYearId" });
            DropIndex("dbo.tblBenefitPlans", new[] { "benefitTypeId" });
            DropIndex("dbo.tblBenefitPlans", new[] { "insCompanyId" });
            DropIndex("dbo.tblBenefitEnrollment", new[] { "benefitPlanId" });
            DropIndex("dbo.tblBenefitEnrollment", new[] { "employeeId" });
            DropIndex("dbo.tblPayrollRecords", new[] { "payrollImportId" });
            DropIndex("dbo.tblEmployeePaidWages", new[] { "payrollRecordId" });
            DropIndex("dbo.tblEmployeePaidWages", new[] { "employeeId" });
            DropIndex("dbo.tblEmployeeComplianceTrackings", new[] { "complianceId" });
            DropIndex("dbo.tblDocuments", new[] { "empComplianceId" });
            DropIndex("dbo.tblEmployeeCompliance", new[] { "compTrackingId" });
            DropIndex("dbo.tblEmployeeCompliance", new[] { "empId" });
            DropIndex("dbo.tblEmergencyContact", new[] { "employeeId" });
            DropIndex("dbo.tblBenefitEligibles", new[] { "employeeId" });
            DropIndex("dbo.tblEmployee", new[] { "genderId" });
            DropIndex("dbo.tblEmployee", new[] { "salutation" });
            DropIndex("dbo.tblJobContracts", new[] { "employeeTypes" });
            DropIndex("dbo.tblJobContracts", new[] { "employeeId" });
            DropIndex("dbo.tblACAs", new[] { "contractId" });
            DropTable("dbo.tblUsersResetPassword");
            DropTable("dbo.tblTerminationReasons");
            DropTable("dbo.tblTahapans");
            DropTable("dbo.tblStages");
            DropTable("dbo.tblSektors");
            DropTable("dbo.tblProjects");
            DropTable("dbo.Educations");
            DropTable("dbo.tblDeclineReasons");
            DropTable("dbo.tblCompanys");
            DropTable("dbo.tblJobPayRates");
            DropTable("dbo.tblWotcTargets");
            DropTable("dbo.tblWotcDocuments");
            DropTable("dbo.tblSalutations");
            DropTable("dbo.tblGenders");
            DropTable("dbo.tblFamilys");
            DropTable("dbo.tblBenefitFamilyTrackingOffers");
            DropTable("dbo.tblInsuranceCompanys");
            DropTable("dbo.tblBenefitTypes");
            DropTable("dbo.tblAPI");
            DropTable("dbo.tblAPIMenu");
            DropTable("dbo.tblMenu");
            DropTable("dbo.tblRoleAccess");
            DropTable("dbo.tblUserRole");
            DropTable("dbo.UserTaskRegardings");
            DropTable("dbo.UserTasks");
            DropTable("dbo.tblMaskingFieldsSetting");
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblRole");
            DropTable("dbo.tblRoleEmployeeTypes");
            DropTable("dbo.tblEmployeeTypess");
            DropTable("dbo.tblPlanYearEmployeeTypes");
            DropTable("dbo.tblBenefitPlanYears");
            DropTable("dbo.tblBenefitPlans");
            DropTable("dbo.tblBenefitEnrollment");
            DropTable("dbo.tblImportPayroll");
            DropTable("dbo.tblPayrollRecords");
            DropTable("dbo.tblEmployeePaidWages");
            DropTable("dbo.tblCompliances");
            DropTable("dbo.tblEmployeeComplianceTrackings");
            DropTable("dbo.tblDocuments");
            DropTable("dbo.tblEmployeeCompliance");
            DropTable("dbo.tblEmergencyContact");
            DropTable("dbo.tblBenefitEligibles");
            DropTable("dbo.tblEmployee");
            DropTable("dbo.tblJobContracts");
            DropTable("dbo.tblACAs");
        }
    }
}
