
// environment.ts (Development environment)
export const environment = {
    production: false,
    coreApiUrl: 'http://localhost:7014/api/',
    UrlConstants: {
        Authenticate: 'useraccess/authenticateuser',
        VerifyAccessToken: 'useraccess/verifyaccestoken',
        GenerateUserClaims: 'useraccess/genarateuserclaims',

        // Customers
        GetCustomers: 'customers/getcustomers',
        GetCustomersById: 'customers/getcustomersbyid',
        SaveCustomers: 'customers/savecustomers',

        // Insurance Policies
        GetInsurancePolicies: 'insurancepolicy/getinsurancepolicies',
        GetInsurancePolicyById: 'insurancepolicy/getinsurancepolicy/{insurancepolicyid}',
        SaveInsurancePolicy: 'insurancepolicy/saveinsurancepolicy',
        UploadInsurancePolicies: 'insurancepolicy/uploadinsurancepolicies',

        // Policy Categories
        GetPolicyCategories: 'policycategory/getpolicycategories',
        GetPolicyCategoryById: 'policycategory/getpolicycategoriesbyid',
        SavePolicyCategory: 'policycategory/savepolicycategory',

        // Policy Types
        GetPolicyTypes: 'policytype/getpolicytypes',
        GetPolicyTypeById: 'policytype/getpolicytypebyid',
        SavePolicyType: 'policttype/savepolicyType',

        // Vehicle Classes
        GetVehicleClasses: 'vehicleclass/getvehicleclasses',
        GetVehicleClassById: 'vehicleclass/getvehicleclassbyid/{vehicleClassId}',
        SaveVehicleClass: 'vehicleclass/savevehicleclass',

        // Vehicle Sizes
        GetVehicleSizes: 'vehiclesize/getvehiclesizes',
        GetVehicleSizeById: 'vehiclesize/getvehiclesizebyid/{vehiclesizeid}',
        SaveVehicleSize: 'vehiclesize/savevehiclesize',

        // Service Types
        GetServiceTypes: 'servicetype/getallservicetypes',
        GetServiceType: 'servicetype/getservicetypebyid',
        SaveServiceType: 'servicetype/saveservicetype',

        //Workflow
        GetWorkflows: 'workflow/getallworkflows',
        SaveWorkflow: 'workflow/saveworkflow',

        //Role
        GetRoles: 'role/getroles',
        SaveRole: 'role/saverole',

        //Service Providers
        GetAllServiceProviders: 'serviceprovider/getallserviceproviders',
        GetServiceProvider: 'serviceprovider/getserviceprovider',
        SaveServiceProvider:'serviceprovider/saveserviceprovider',

         //Service Providers
         GetUsers: 'user/getusers',
         GetUserById: 'user/getuserbyid',
         SaveUser:'user/saveuser'

    },
    Messages: {
        InvalidPassword: "Authentication Failed due to Invalid Password ,Please enter correct password and try again",
        InValidUserName: "Authentication Failed due to Invalid UserName ,Please enter correct username and try again",
        UserWasBlocked: "User Was blocked",
        SomethingwentWrong: "Something went wrong,Please try after sometime"
    },
    Titles: {
        InvalidPassword: "Wrong/Invalid Password",
        InValidUserName: "Worng/Invalid Username",
        UserWasBlocked: "User blocked",
        SomethingWentWrong: "Something Went wrong"
    },
    ErrorCodes: {
        EC1001: 1001,
        EC1002: 1002,
        EC1003: 1003,
        EC1004: 1004
    },
    AppConstants: {
        Roles: {
            DigitalSalesMarketing: "Digital Sales Marketing",
            ProductManager: "Product Manager",
            CustomerServiceRepresentative: "Customer Service Representative",
            Customer: "Customer",
            Developer: "Developer",
            EcommerceManager: "E-commerce Manager",
            WebDeveloper: "Web Developer",
            OperationManager: "Operation Manager",
            GraphicDesigner: "Graphic Designer",
            DataAnalyst: "Data Analyst",
            FashionDesigner: "Fashion Designer",
            Administrator: "Administrator",
            SoftwareEngineer: "Software Engineer",
            User: "User",
            Executive: "Executive",
            Seller: "Seller",
        },
        departments: {
            Sales: "Sales",
        },
        FashionDesignerType: {
            ReadyToWear: "Ready-to-wear",
            EconomyFashion: "Economy fashion",
            HauteCouture: "Haute couture",
            TextileDesigner: "Textile Designer",
            FootwearDesigners: "Footwear designers",
            GraphicDesigner: "Graphic Designer",
            AccessoryDesigner: "Accessory designer",
            AthleticWear: "Athletic wear",
            FastFashion: "Fast fashion",
            MassMarketDesigners: "Mass market designers",
            KidswearDesigners: "Kidswear designers",
            FashionMarketer: "Fashion marketer",
            FashionWriter: "Fashion writer",
            LimitedEdition: "Limited edition",
            Merchandiser: "Merchandiser",
            SpecializedAreasOfFashionDesign: "Specialized areas of fashion design",
            Shirts: "Shirts",
            Jeans: "Jeans",
            Swimwear: "Swimwear",
            Sleepwear: "Sleepwear",
            Sportswear: "Sportswear",
            Jumpsuits: "Jumpsuits",
            Blazers: "Blazers",
            Jackets: "Jackets",
            Shoes: "Shoes",
            MensDresses: "Men's Dresses",
            WomensDresses: "Women's Dresses",
            BabysDresses: "Baby's Dresses",
        },
        Countries: {
            Afghanistan: "Afghanistan",
            Albania: "Albania",
            Algeria: "Algeria",
            Argentina: "Argentina",
            Australia: "Australia",
            Bangladesh: "Bangladesh",
            "USA": "United States of America",
            India: "India",
            Pakistan: "Pakistan",
            "People Republic of China": "People Republic of China",
            Singapore: "Singapore",
            Dubai: "Dubai",
            SaudiArabia: "Saudi Arabia",
            Bangkok: "Bangkok",
        },
        States: {
            TELANGANA: "Telangana",
            ANDHRAPRADESH: "Andhra Pradesh",
            KARNATAKA: "Karnataka",
            MAHARASTRA: "Maharashtra",
            UTTERPRADESH: "Uttar Pradesh",
            MADYAPRADESH: "Madhya Pradesh",
            TAMILNADU: "Tamil Nadu",
            KERALA: "Kerala",
            GOA: "Goa",
            ORISSA: "Odisha",
            PANJAB: "Punjab",
            SINDHU: "Sindh",
            Alabama: "Alabama",
            Alaska: "Alaska",
            Arizona: "Arizona",
            Arkansas: "Arkansas",
            California: "California",
            Colorado: "Colorado",
            Connecticut: "Connecticut",
            HARYANA: "Haryana",
            JAMMUKASHMIR: "Jammu & Kashmir",
            Srinagar: "Srinagar",
        },
        Cities: {
            HYDERABAD: "Hyderabad",
            WARANGAL: "Warangal",
            KARIMNAGAR: "Karimnagar",
            KHAMMAM: "Khammam",
            NIZAMABAD: "Nizamabad",
            MEDAK: "Medak",
            AMARAVATHI: "Amaravathi",
            VISHAKAPATNAM: "Vishakapatnam",
            THIRUPATHI: "Thirupathi",
            VijayaWada: "Vijayawada",
            California: "California",
            Bhadrachalam: "Bhadrachalam",
            Hanmakonda: "Hanmakonda",
            BhadraChalam1: "BhadraChalam1",
            BCM: "BCM",
            Venkatapuram: "Venkatapuram",
            vpm: "vpm",
        },
        Status: {
            "New": "New",
            "Created": "Created"
        },
        Braintree: {
            environment: 'braintree.Environment.Sandbox',
            merchantId: 'q577ct9g9qyq8jt7',
            publicKey: 'bdgysbrrb98f7kb7',
            privateKey: '11568834eefc8a3448a59ac682a34a63',
        }
    }
};
