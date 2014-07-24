using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class AccountBaseEventSrc : SourceEvent
    {
        //NOT NULL = AccountID, OwnerId, OwnerIdType
        public Guid AccountId{ get; set; }
        public int? AccountCategoryCode { get; set; }
        public Guid? TerritoryId { get; set; }
        public Guid? DefaultPriceLevelId { get; set; }
        public int? CustomerSizeCode { get; set; }
        public int? PreferredContactMethodCode { get; set; }
        public int? CustomerTypeCode { get; set; }
        public int? AccountRatingCode { get; set; }
        public int? IndustryCode { get; set; }
        public int? TerritoryCode { get; set; }
        public int? AccountClassificationCode { get; set; }
        public int? BusinessTypeCode { get; set; }
        public Guid? OwningBusinessUnit { get; set; }
        public Guid? OriginatingLeadId { get; set; }
        public int? PaymentTermsCode { get; set; }
        public int? ShippingMethodCode { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public bool? ParticipatesInWorkflow { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Revenue { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string Description { get; set; }
        public string SIC { get; set; }
        public int? OwnershipCode { get; set; }
        public decimal? MarketCap { get; set; }
        public int? SharesOutstanding { get; set; }
        public string TickerSymbol { get; set; }
        public string StockExchange { get; set; }
        public string WebSiteURL { get; set; }
        public string FtpSiteURL { get; set; }
        public string EMailAddress1 { get; set; }
        public string EMailAddress2 { get; set; }
        public string EMailAddress3 { get; set; }
        public bool? DoNotPhone { get; set; }
        public bool? DoNotFax { get; set; }
        public string Telephone1 { get; set; }
        public bool? DoNotEMail { get; set; }
        public string Telephone2 { get; set; }
        public string Fax { get; set; }
        public string Telephone3 { get; set; }
        public bool? DoNotPostalMail { get; set; }
        public bool? DoNotBulkEMail { get; set; }
        public bool? DoNotBulkPostalMail { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool? CreditOnHold { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Byte[] VersionNumber { get; set; }
        public Guid? ParentAccountId { get; set; }
        public decimal? Aging30 { get; set; }
        public int? StateCode { get; set; }
        public decimal? Aging60 { get; set; }
        public int? StatusCode { get; set; }
        public decimal? Aging90 { get; set; }
        public int? PreferredAppointmentDayCode { get; set; }
        public Guid? PreferredSystemUserId { get; set; }
        public int? PreferredAppointmentTimeCode { get; set; }
        public bool? Merged { get; set; }
        public bool? DoNotSendMM { get; set; }
        public Guid? MasterId { get; set; }
        public DateTime? LastUsedInCampaign { get; set; }
        public Guid? PreferredServiceId { get; set; }
        public Guid? PreferredEquipmentId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? UTCConversionTimeZoneCode { get; set; }
        public DateTime? OverriddenCreatedOn { get; set; }
        public int? TimeZoneRuleVersionNumber { get; set; }
        public int? ImportSequenceNumber { get; set; }
        public Guid? TransactionCurrencyId { get; set; }
        public decimal? CreditLimit_Base { get; set; }
        public decimal? Aging30_Base { get; set; }
        public decimal? Revenue_Base { get; set; }
        public decimal? Aging90_Base { get; set; }
        public decimal? MarketCap_Base { get; set; }
        public decimal? Aging60_Base { get; set; }
        public string YomiName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? ModifiedOnBehalfBy { get; set; }
        public Guid? CreatedOnBehalfBy { get; set; }
        public int OwnerIdType { get; set; }
        public Guid? EntityImageId { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? StageId { get; set; }
        public string new_ManagedTerritory { get; set; }
        public string new_AccountTS { get; set; }
        public Guid? mbs_SalesDistrictId { get; set; }
        public Guid? mbs_stateid { get; set; }
        public int? mbs_serverstotal { get; set; }
        public string new_ATUName { get; set; }
        public string new_Industry { get; set; }
        public string mbs_tenantid { get; set; }
        public string mbs_crmrecorduniqueid { get; set; }
        public int? new_CRMServerLicenses { get; set; }
        public int? mbs_Segment { get; set; }
        public Guid? mbs_SalesSubDistrictId { get; set; }
        public int? mbs_LeadCountry { get; set; }
        public string new_ATUGroupName { get; set; }
        public Guid? mbs_hierarchylevelid { get; set; }
        public int? mbs_pcdevicestotal { get; set; }
        public string new_Vertical { get; set; }
        public string new_VerticalCategory { get; set; }
        public int? mbs_StateProvidence { get; set; }
        public string new_SubSegment { get; set; }
        public string new_Segment { get; set; }
        public decimal? mbs_itbudget_Base { get; set; }
        public string mbs_msorgid { get; set; }
        public int? mbs_SubSegment { get; set; }
        public string new_AccountManager { get; set; }
        public Guid? mbs_countryid { get; set; }
        public string new_ManagedVertical { get; set; }
        public Guid? mbs_industryid { get; set; }
        public decimal? mbs_itbudget { get; set; }
        public string mbs_mioid { get; set; }
        public string new_MSSalesTPID { get; set; }
        public int? new_CRMUserLicences { get; set; }
        public DateTime? mbs_fiscalcalendarstart { get; set; }
        public string new_GSXAccountID { get; set; }
        public string mbs_dunsnumber { get; set; }
        public int? mbs_pccounttotalorg { get; set; }
        public string new_ATUManager { get; set; }
        public string mbs_TaxID { get; set; }



        public static string getPrimaryKey()
        {
            return "AccountId";
        }

     
    }
}
