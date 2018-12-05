
export interface MainDatabasesModel {
    MainDatabasesModelid: string;
    IDataSource: string;
    IInitialCatalog: string;
    IUsername: string;
    IPassword: string;
    IName: string;
    IUIDCode: string;
    isPublished: boolean;
    remarks: string;
}

export interface Credentials {
    email: string;  
    password: string;
    confirmpassword: string;
}

export interface newuser {
    UserID: string;
    Mobile: string;
    Password: string;
    ConfirmPassword: string;
    Email: string;
    UserRole: string;
    nameofuser: string;
    remarks: string;
}

export interface ChangepasswordModel {
    OldPassword: string;
    NewPassword: string;
    Confirmpassword: string;
}

export interface ClientDetailModel {
    ClientDetailModelid: string;
    productname: string;
    productid: string;
    businessname: string;
    clientaddress: string;
    expirydate: Date;
    Mobile: string;
    Email: string;
    isPublished: boolean;
    remarks: string;
    isExpired: boolean;
}

export interface ClientDetailModelToShow {
    ClientDetailModel: ClientDetailModel;
    isExpired: boolean;
}

export interface PageModel {
    PageModelid: string;
    PageTitle: string;
    pageurl: string;
    pagecontent: string;
    isPublished: boolean;
    remarks: string;
    pcontents: PageContentModel[];
    Layout: LayoutModel;
}

export interface PageContentModel {
    PCType: string;
    PContent: string;
}

export interface RolesModel {
    Id: string;
    Name: string;
}

export interface MenuModel {
    MenuModelid: string;
    MenuName: string;
    MenuClass: string;
    isPublished: boolean;
    remarks: string;
    
}

export interface LayoutModel {
    LayoutModelid: string;
    LayoutName: string;
    isPublished: boolean;
    remarks: string;
    MenuModelid: string;
}


export interface MenuItemModel {
    MenuItemModelid: string;
    ItemTitle: string;
    ItemText: string;
    ItemLink: string;
    Classli: string;
    Classa: string;
    Parentid: string;
    MenuModelid: string;
    isPublished: boolean;
    remarks: string;
    itemPriority: number;
}


export interface AlbumModel {
    AlbumModelid: string;
    AlbumName: string;
    isPublished: boolean;
    remarks: string;
}

export interface ImageGalleryModel {
    ImageGalleryModelid: string;
    ImageName: string;
    isPublished: boolean;
    remarks: string;
    AlbumModelid: string;
}

export interface SiteModel {
    SiteModelid: string;
    SiteLogo: string;
    remarks: string;
}

export interface  RolesDetailModel {
    UserID: string;
    Rolenames: string[];
}

//export interface NotificationModel {
//    NotificationModelid: string;
//    title: string;
//    description: string;
//    imagepath: string;
//    isPublished: boolean;
//    remarks: string;
//    BlogTypeName: string;
//}




export interface NotificationScheduleModel {
    NotificationScheduleModelid: string;
    notificationsentdate: Date;
    classname: string;
    isPublished: boolean;
    remarks: string;
    BlogModelid: string;
}

export interface BlogTypeModel {
    BlogTypeModelid: string;
    BlogTypeDisplayName: string;
    BlogTypeName: string;
    isPublished: boolean;
    remarks: string;
    BlogTypeProperty: string;
}

export interface BlogModel {
    BlogModelid: string;
    title: string;
    description: string;
    imagepath: string;
    isPublished: boolean;
    remarks: string;
    BlogType: BlogTypeModel;
    SubjectModelid: string;

}

export interface TeacherBlogModel {
    BlogModelid: string;
    TeacherId: string;
    }

export interface TCSSRelModel {
    TCSSRelModelid: string;
    Teacherid: string;
    ClassModelid: string;
    ForClass: ClassModel;
    SectionModelid: string;
    ForSection: SectionModel;
    SubjectModelid: string;
    ForSubject: SubjectModel;
    isPublished: boolean;
    remarks: string;
}


export interface ClassModel {
    ClassModelid: string;
}

export interface SubjectModel {
    SubjectModelid: string;
}

export interface SectionModel {
    SectionModelid: string;
}

export interface StudentModel {
    StudentModelID: string;
    strSchoolName: string;
    strAdmissionNo: string;
    strAcNo: string;
    strStudentName: string;
    strFatherName: string;
    strMotherName: string;
    ClassModelid: string;
    OfClass: ClassModel;
    strSex: string;
    numMobileNoForSms: string;
    SectionModelid: string;
    OfSection: SectionModel;
}


export interface ClassSectionRel {
    ClassModelid: string;
    SectionModelid: string;
    ClassModelName: string;
    SectionModelName: string;
}

export interface NotificationToClassSection {
    typeofnotice: string;
    noticetoclasssection: ClassSectionRel[];
}

export interface FeedbackModel {
    FeedbackModelid: string;
    FeedbackType: string;
    StudentModelID: number;
    FeedbackDate: Date;
    FeedbackMessage: string;
    isPublished: boolean;
    remarks: string;
}



