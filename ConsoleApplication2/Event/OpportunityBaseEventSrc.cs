﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class OpportunityBaseEventSrc
    {
        public Guid? OpportunityId  {get; set;}
        public Guid? PriceLevelId {get; set;}
        public int? OpportunityRatingCode {get; set;}
        public int? PriorityCode {get; set;}
        public string Name {get; set;}
        public Guid? StepId {get; set;}
        //public string Description {get; set;}
        public decimal? EstimatedValue {get; set;}
        public string StepName {get; set;}
        public int? SalesStageCode {get; set;}
        public bool? ParticipatesInWorkflow {get; set;}
        public int? PricingErrorCode {get; set;}
        public DateTime? EstimatedCloseDate {get; set;}
        public int? CloseProbability {get; set;}
        public decimal? ActualValue {get; set;}
        public DateTime? ActualCloseDate {get; set;}
        public Guid? OwningBusinessUnit {get; set;}
        public Guid? OriginatingLeadId {get; set;}
        public DateTime? CreatedOn {get; set;}
        public bool? IsPrivate {get; set;}
        public Guid? CreatedBy {get; set;}
        public DateTime? ModifiedOn {get; set;}
        public Guid? ModifiedBy {get; set;}
        public Byte[] VersionNumber {get; set;}
        public int? StateCode  {get; set;}
        public int? StatusCode {get; set;}
        public bool? IsRevenueSystemCalculated {get; set;}
        public Guid? CampaignId {get; set;}
        public Guid? TransactionCurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? ImportSequenceNumber { get; set; }
        public int? UTCConversionTimeZoneCode { get; set; }
        public int? TimeZoneRuleVersionNumber { get; set; }
        public DateTime? OverriddenCreatedOn { get; set; }
        public decimal? ActualValue_Base { get; set; }
        public decimal? EstimatedValue_Base { get; set; }
        public decimal? TotalDiscountAmount { get; set; }
        public Guid? ModifiedOnBehalfBy { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedOnBehalfBy { get; set; }
        public decimal? TotalAmountLessFreight { get; set; }
        public decimal? TotalLineItemDiscountAmount { get; set; }
        public Guid? CustomerId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public Guid? OwnerId { get; set; }
        public decimal? FreightAmount { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? TotalLineItemAmount { get; set; }
        public string CustomerIdName { get; set; }
        public int? CustomerIdType { get; set; }
        public int? OwnerIdType { get; set; }
        public decimal? TotalDiscountAmount_Base { get; set; }
        public decimal? FreightAmount_Base { get; set; }
        public decimal? TotalLineItemAmount_Base { get; set; }
        public decimal? TotalTax_Base { get; set; }
        public decimal? TotalLineItemDiscountAmount_Base { get; set; }
        public decimal? TotalAmount_Base { get; set; }
        public decimal? DiscountAmount_Base { get; set; }
        public decimal? TotalAmountLessFreight_Base { get; set; }
        public string CustomerIdYomiName { get; set; }
        public bool? FileDebrief { get; set; }
        public int? BudgetStatus { get; set; }
        public bool? PresentProposal { get; set; }
        public bool? SendThankYouNote { get; set; }
        public bool? EvaluateFit { get; set; }
        public DateTime? FinalDecisionDate { get; set; }
        public bool? ConfirmInterest { get; set; }
        public bool? CompleteInternalReview { get; set; }
        public int? TimeLine { get; set; }
        public string CustomerPainPoints { get; set; }
        public bool? ResolveFeedback { get; set; }
        public int? PurchaseProcess { get; set; }
        public bool? CaptureProposalFeedback { get; set; }
        public bool? IdentifyCustomerContacts { get; set; }
        public bool? CompleteFinalProposal { get; set; }
        public string QuoteComments { get; set; }
        public decimal? BudgetAmount { get; set; }
        public DateTime? ScheduleFollowup_Qualify { get; set; }
        public int? Need { get; set; }
        public bool? PursuitDecision { get; set; }
        public Guid? ParentAccountId { get; set; }
        public DateTime? ScheduleProposalMeeting { get; set; }
        public string QualificationComments { get; set; }
        public int? SalesStage { get; set; }
        public Guid? ParentContactId { get; set; }
        public int? InitialCommunication { get; set; }
        public bool? IdentifyPursuitTeam { get; set; }
        public DateTime? ScheduleFollowup_Prospect { get; set; }
        public int? PurchaseTimeframe { get; set; }
        public bool? IdentifyCompetitors { get; set; }
        public bool? DecisionMaker { get; set; }
        public string CurrentSituation { get; set; }
        public string CustomerNeed { get; set; }
        public string ProposedSolution { get; set; }
        public bool? PresentFinalProposal { get; set; }
        public bool? DevelopProposal { get; set; }
        public decimal? BudgetAmount_Base { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? StageId { get; set; }
        public Guid? mbs_opportunitylicenseprogramid { get; set; }
        public string mbs_businesspriority { get; set; }
        public string mbs_RCode { get; set; }
        public string mbs_NotesfromPartner { get; set; }
        public bool? mbs_revenue { get; set; }
        public int? new_TimeToPurchase { get; set; }
        public Guid? mbs_subscriptionid { get; set; }
        public Guid? new_ProductId { get; set; }
        public decimal? mbs_netextendedprice { get; set; }
        public int? new_NoOfEmployees { get; set; }
        public Guid? mbs_billingfrequencyid { get; set; }
        public decimal? mbs_syscaltotalestrevph_Base { get; set; }
        public Guid? mbs_opportunitylicensesubtypeid { get; set; }
        public bool? mbs_processed { get; set; }
        public Guid? new_PartnerInvolvedId { get; set; }
        public int? new_TotalNoOfPCs { get; set; }
        public string mbs_crmrecorduniqueid { get; set; }
        public int? mbs_opportunitytype { get; set; }
        public DateTime? mbs_processeddate { get; set; }
        public string mbs_gsxopportunityid { get; set; }
        public decimal? mbs_OpportunityValue { get; set; }
        public decimal? mbs_opportunityvalue_Base { get; set; }
        public string mbs_notestopartner { get; set; }
        public string mbs_processedby { get; set; }
        public bool? mbs_customerconsentflag { get; set; }
        //public string mbs_activityinfo { get; set; }
        //public bool? mbs_sendtogsx { get; set; }
        //public int? new_SalesStage { get; set; }
        //public int? mbs_partnerengagementtype { get; set; }
        //public Guid? mbs_gsxownerid { get; set; }
        //public bool? new_Budget { get; set; }
        //public int? mbs_opportunityrouting { get; set; }
        //public Guid? mbs_contactid { get; set; }
        //public string mbs_topquestionsandconcerns { get; set; }
        //public decimal? mbs_SysCalTotalEstRevPH { get; set; }
        //public int? mbs_EstimatedNoOfSeats { get; set; }
        //public bool? mbs_validated { get; set; }
        //public string mbs_leadqualificationreason { get; set; }
        //public string mbs_mqpstatus { get; set; }
        //public Guid? mbs_promocodeid { get; set; }
        //public decimal? mbs_netextendedprice_Base { get; set; }
        //public int? mbs_wwlmstatus { get; set; }
        //public bool? mbs_winlossreview { get; set; }
    //    public string mbs_migrationnotes {get; set;}
    //    public Guid? mbs_ForecastFlagId {get; set;}
    //    public int? mbs_customeragreement {get; set;}
    //    public int? mbs_internalagreement {get; set;}
    //    public int? mbs_partnerfunding {get; set;}
    //    public Guid? mbs_apengagementtypeid {get; set;}
    }
}
