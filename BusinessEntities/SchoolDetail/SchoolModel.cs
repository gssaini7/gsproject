
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace R.BusinessEntities
{
    public class StudentModel : EntityCommonModel
    {
        public int StudentModelID { get; set; }
        //public int numAdmissionNo { get; set; }
        //public string strSchoolName { get; set; }
        public string strAdmissionNo { get; set; }
        public string strAcNo { get; set; }
        public string strStudentName { get; set; }
        public string strFatherName { get; set; }
        public string strMotherName { get; set; }

        public int ClassModelid { get; set; }
        [ForeignKey("ClassModelid")]
        public ClassModel OfClass { get; set; }

        public string strAddress { get; set; }
        public string strSex { get; set; }
        public string numMobileNoForSms { get; set; }

        public int? SectionModelid { get; set; }
        [ForeignKey("SectionModelid")]
        public SectionModel OfSection { get; set; }

        public int? BranchModelid { get; set; }
        [ForeignKey("BranchModelid")]
        public BranchModel OfBranch { get; set; }

        public string strRollNo { get; set; }

        public string strDistrict { get; set; }
        public string strZipCode { get; set; }
        public string strEmail { get; set; }


        public int? HouseModelid { get; set; }
        [ForeignKey("HouseModelid")]
        public HouseModel OfHouse { get; set; }

        //public string strHouseCode { get; set; }/////////////////////////

        public DateTime? dob_student { get; set; }
        public DateTime? dob_father { get; set; }
        public DateTime? dob_mother { get; set; }
        public DateTime? dob_anniversary { get; set; }


        public int? VehicleModelid { get; set; }
        [ForeignKey("VehicleModelid")]
        public VehicleModel VehicleDetail { get; set; }
        //public string vehiclenumber { get; set; }/////////////////////////////////


    }


    public class AttendanceModel : EntityCommonModel
    {
        public int AttendanceModelID { get; set; }
        public DateTime attendancedate { get; set; }
        public string status { get; set; }

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ForStudent { get; set; }
    }

    public class LedgerDetailModel : EntityCommonModel
    {
        public int LedgerDetailModelID { get; set; }
        //public int numAdmissionNo { get; set; }
        public string strPeriod  { get; set; }

        [Column(TypeName = "Money")]
        public decimal numTotalDue { get; set; }
        [Column(TypeName = "Money")]
        public decimal numTotalPaid { get; set; }
        [Column(TypeName = "Money")]
        public decimal numTotalBalance { get; set; }

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ForStudent { get; set; }
    }

    public class LedgerSumaryModel : EntityCommonModel
    {
        public int LedgerSumaryModelID { get; set; }
        //public int numAdmissionNo { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalDue { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalPaid { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalBalance { get; set; }

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ForStudent { get; set; }
    }

    public class AcademicModel : EntityCommonModel
    {
        public int AcademicModelID { get; set; }
        public string strTotalMarks { get; set; }
        public string strTotalGrade { get; set; }
       
        //public string strTerm { get; set; }
        //public string strAssessment { get; set; }
        //public string strSubject { get; set; }
       

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ForStudent { get; set; }

        public int TermModelid { get; set; }
        [ForeignKey("TermModelid")]
        public TermModel ForTerm { get; set; }

        public int SubjectModelid { get; set; }
        [ForeignKey("SubjectModelid")]
        public SubjectModel ForSubject { get; set; }
    }

    public class AcademicDetailModel : EntityCommonModel
    {
        public int AcademicDetailModelid { get; set; }
        public string strMarks { get; set; }
        public string strGrade { get; set; }

        //public int AcademicModelID { get; set; }
        //[ForeignKey("AcademicModelID")]
        //public AcademicModel ForAcademics { get; set; }

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ForStudent { get; set; }

        public int TermModelid { get; set; }
        [ForeignKey("TermModelid")]
        public TermModel ForTerm { get; set; }

        public int SubjectModelid { get; set; }
        [ForeignKey("SubjectModelid")]
        public SubjectModel ForSubject { get; set; }

        public int? AssesmentModelid { get; set; }
        [ForeignKey("AssesmentModelid")]
        public AssesmentModel TypeOfAssesment { get; set; }
    }

    public class TermModel : EntityCommonModel
    {
        public int TermModelid { get; set; }
        public string TermName { get; set; }
    }

    public class AssesmentModel : EntityCommonModel
    {
        public int AssesmentModelid { get; set; }
        public string AssesmentName { get; set; }
    }


    public class SchoolModel : EntityCommonModel
    {
        public int SchoolModelID { get; set; }
        public string strSchoolName { get; set; }
        public string strAddress { get; set; }
        public string strDistrict { get; set; }
        public string strState { get; set; }
        public string strAffiliatedTo { get; set; }
        public string strAffiliationNo { get; set; }
        public string strEmail { get; set; }
        public string strWebsite { get; set; }
    }

    public class ClassModel : EntityCommonModel
    {
        public int ClassModelid { get; set; }
        public string ClassName { get; set; }
        public string ClassDetail { get; set; }

    }

    public class SectionModel : EntityCommonModel
    {
        public int SectionModelid { get; set; }
        public string SectionName { get; set; }

    }

    public class SubjectModel : EntityCommonModel
    {
        public int SubjectModelid { get; set; }
        public string SubjectName { get; set; }

    }

    public class HouseModel : EntityCommonModel
    {
        public int HouseModelid { get; set; }
        public string HouseName { get; set; }

    }

    public class VehicleModel : EntityCommonModel
    {
        public int VehicleModelid { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string DriverName { get; set; }
        public string ContactNumber { get; set; }
    }

    public class BranchModel : EntityCommonModel
    {
        public int BranchModelid { get; set; }
        public string BranchName { get; set; }

    }

    // teacher, class, section, subject relation
    public class TCSSRelModel:EntityCommonModel
    { 
        public Guid TCSSRelModelid { get; set; }
        public Guid Teacherid { get; set; }

        public int ClassModelid { get; set; }
        [ForeignKey("ClassModelid")]
        public ClassModel ForClass { get; set; }

        public int SectionModelid { get; set; }
        [ForeignKey("SectionModelid")]
        public SectionModel ForSection { get; set; }

        public int? SubjectModelid { get; set; }
        [ForeignKey("SubjectModelid")]
        public SubjectModel ForSubject { get; set; }
    }

    public class ClassSectionModel 
    {
        public int ClassModelid { get; set; }
        public int SectionModelid { get; set; }
    }

    public class ClassSectionSubjectModel:ClassSectionModel
    {
        public int SubjectModelid { get; set; }
    }

    public class FeedbackModel : EntityCommonModel
    {
        public Guid FeedbackModelid { get; set; }
        public string FeedbackType { get; set; }

        public int StudentModelID { get; set; }
        [ForeignKey("StudentModelID")]
        public StudentModel ParentOfStudent { get; set; }

        public DateTime FeedbackDate { get; set; }
        public DateTime FeedbackOpenDate { get; set; }


        public string FeedbackMessage { get; set; }

    }

    public class SettingsModel
    {
        public Guid SettingsModelid { get; set; }
        public string SettingsType { get; set; }
        public string SettingsContent { get; set; }
    }

    public class SMSModel
    {
        public string username { get; set; }
        public string msgtoken { get; set; }
        public string senderid { get; set; }
        public string apiurl { get; set; }
    }

    public class MailModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string smtp { get; set; }
        public string port { get; set; }
    }

    public class OnlinePaymentModel
    {
        public string api_key { get; set; }
        public string salt { get; set; }
        public string url { get; set; }
    }


    public class AcademicDetailConditionsModel
    {
        public int StudentModelID { get; set; }
        public int TermModelid { get; set; }
        public int SubjectModelid { get; set; }
    }


    public class OnlinePaymentAppDetailModel
    {
        public int StudentModelID { get; set; }
        public double feeamount { get; set; }
        public string remarks { get; set; }
    }

    //public class OnlinePaymentVariables
    //{
    //    remotepost.Url = "https://biz.traknpay.in/v1/paymentrequest";
    //        remotepost.Add("api_key", request["api_key"]);
    //        remotepost.Add("return_url", request["return_url"]);
    //        remotepost.Add("mode", request["mode"]);
    //        remotepost.Add("order_id", request["order_id"]);
    //        remotepost.Add("amount", request["amount"]);
    //        remotepost.Add("name", request["name"]);
    //        remotepost.Add("currency", request["currency"]);
    //        remotepost.Add("description", request["description"]);
    //        remotepost.Add("address_line_1", request["address_line_1"]);
    //        //remotepost.Add("address_line_2", request["address_line_2"]);
    //        remotepost.Add("phone", request["phone"]);
    //        remotepost.Add("email", request["email"]);
    //        remotepost.Add("city", request["city"]);
    //        //remotepost.Add("state", request["state"]);
    //        remotepost.Add("country", request["country"]);
    //        remotepost.Add("zip_code", request["zip_code"]);
    //        remotepost.Add("udf1", request["udf1"]);
    //        //remotepost.Add("udf2", request["udf2"]);
    //        //remotepost.Add("udf3", request["udf3"]);
    //        //remotepost.Add("udf4", request["udf4"]);
    //        //remotepost.Add("udf5", request["udf5"]);
    //        remotepost.Add("hash", gethash(hash_columns, request, payment.salt));
    //}
}
