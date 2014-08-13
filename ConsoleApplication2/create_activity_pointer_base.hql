
SET hbase.zookeeper.quorum=zookeepernode0,zookeepernode1,zookeepernode2;  
CREATE EXTERNAL TABLE ActivityPointerBase(
		ActivityId string ,
		PhoneCategory string,
		PhoneSubcategory string,
		PhoneNumber string,
		OwningBusinessUnit string,
		ActualEnd string,
		VersionNumber string,
		IsBilled boolean,
		CreatedBy string,
		ModifiedOn string,
		ServiceId string,
		ActivityTypeCode int ,
		StateCode int ,
		ScheduledEnd string,
		ScheduledDurationMinutes int,
		ActualDurationMinutes int,
		StatusCode int,
		ActualStart string,
		CreatedOn string,
		PriorityCode int,
		RegardingObjectId string,
		Subject string,
		IsWorkflowCreated boolean,
		ScheduledStart string,
		ModifiedBy string,
		RegardingObjectTypeCode int,
		RegardingObjectIdName string,
		SeriesStatus boolean,
		ExpansionStateCode int,
		OwnerId string ,
		InstanceTypeCode int ,
		SeriesId string,
		TransactionCurrencyId string,
		ExchangeRate double,
		IsRegularActivity boolean ,
		OriginalStartDate string,
		ModifiedOnBehalfBy string,
		OwnerIdType int ,
		QteCloseOverriddenCreatedOn string,
		QuoteNumber string,
		QteCloseImportSequenceNumber int,
		QteCloseCategory string,
		QteCloseRevision int,
		QteCloseSubcategory string,
		ApptCategory string,
		ApptGlobalObjectId string,
		ApptIsAllDayEvent boolean,
		ApptImportSequenceNumber int,
		ApptOutlookOwnerApptId int,
		ApptOverriddenCreatedOn string,
		ApptSubcategory string,
		ApptSubscriptionId string,
		ApptLocation string,
		ActualCost_Base double,
		CampActImportSequenceNumber int,
		BudgetedCost_Base double,
		ActualCost double,
		IgnoreInactiveListMembers boolean,
		DoNotSendOnOptOut boolean,
		TypeCode int,
		CampActSubcategory string,
		CampActOverriddenCreatedOn string,
		ExcludeIfContactedInXDays int,
		CampActCategory string,
		BudgetedCost double,
		CampActChannelTypeCode int,
		FirstName string,
		ReceivedOn string,
		ResponseCode int,
		YomiLastName string,
		CampResOverriddenCreatedOn string,
		YomiFirstName string,
		CompanyName string,
		CampResCategory string,
		Telephone string,
		OriginatingActivityId string,
		Fax string,
		LastName string,
		CampResImportSequenceNumber int,
		OriginatingActivityIdTypeCode int,
		EMailAddress string,
		CampResChannelTypeCode int,
		YomiCompanyName string,
		PromotionCodeName string,
		CampResSubcategory string,
		SuccessCount int,
		OperationTypeCode int,
		BulkOperationNumber string,
		TargetMembersCount int,
		CreatedRecordTypeCode int,
		Parameters string,
		ErrorNumber int,
		TargetedRecordTypeCode int,
		FailureCount int,
		Compressed boolean,
		ReadReceiptRequested boolean,
		DeliveryReceiptRequested boolean,
		EmailSubcategory string,
		Notifications int,
		MessageId string,
		Sender string,
		ToRecipients string,
		EmailOverriddenCreatedOn string,
		SubmittedBy string,
		EmailImportSequenceNumber int,
		EmailDirectionCode boolean,
		MimeType string,
		MessageIdDupCheck string,
		DeliveryAttempts int,
		TrackingToken string,
		EmailCategory string,
		SvcApptImportSequenceNumber int,
		SvcApptLocation string,
		SvcApptIsAllDayEvent boolean,
		TimeSpent int,
		CompetitorId string,
		OppCloseOverriddenCreatedOn string,
		OppCloseImportSequenceNumber int,
		ActualRevenue_Base double,
		ActualRevenue double,
		OppCloseSubcategory string,
		OppCloseCategory string,
		IsMapiPrivate boolean,
		LeftVoiceMail boolean,
		EmailAttachmentCount int ,
		ConversationIndex string,
		InReplyTo string,
		CorrelationMethod int,
		BaseConversationIndexHash int,
		ParentActivityId string,
		SenderMailboxId string,
		DeliveryLastAttemptedOn string,
		StageId string,
		SentOn string,
		DeliveryPriorityCode int,
		PostponeActivityProcessingUntil string,
		ProcessId string,
		PostMessageType int,
		ImportSequenceNumber int,
		InResponseTo string,
		PostAuthor string,
		PostedOn string,
		OverriddenCreatedOn string,
		ThreadId string,
		SocialAdditionalParams string,
		PostURL string,
		PostFromProfileId string,
		SocialActivityDirectionCode boolean,
		PostId string,
		SentimentValue float,
		PostAuthorAccount string,
		PostToProfileId string,
		PostAuthorAccountName string,
		PostAuthorAccountType int,
		PostAuthorType int,
		PostAuthorName string,
		PostAuthorYomiName string,
		PostAuthorAccountYomiName string,
		Community int,
		EmailSender string,
		SendersAccount string,
		EmailSenderObjectTypeCode int,
		EmailSenderName string,
		SendersAccountName string,
		SendersAccountObjectTypeCode int,
		EmailSenderYomiName string,
		SendersAccountYomiName string)
STORED BY 'org.apache.hadoop.hive.hbase.HBaseStorageHandler'
WITH SERDEPROPERTIES ('hbase.columns.mapping' = ':key, cf:PhoneCategory, cf:PhoneSubcategory, cf:PhoneNumber, cf:OwningBusinessUnit, cf:ActualEnd, cf:VersionNumber, cf:IsBilled, cf:CreatedBy, cf:ModifiedOn, cf:ServiceId, cf:ActivityTypeCode, cf:StateCode, cf:ScheduledEnd, cf:ScheduledDurationMinutes, cf:ActualDurationMinutes, cf:StatusCode, cf:ActualStart, cf:CreatedOn, cf:PriorityCode, cf:RegardingObjectId, cf:Subject, cf:IsWorkflowCreated, cf:ScheduledStart, cf:ModifiedBy, cf:RegardingObjectTypeCode, cf:RegardingObjectIdName, cf:SeriesStatus, cf:ExpansionStateCode, cf:OwnerId, cf:InstanceTypeCode, cf:SeriesId, cf:TransactionCurrencyId, cf:ExchangeRate, cf:IsRegularActivity, cf:OriginalStartDate, cf:ModifiedOnBehalfBy, cf:OwnerIdType, cf:QteCloseOverriddenCreatedOn, cf:QuoteNumber, cf:QteCloseImportSequenceNumber, cf:QteCloseCategory, cf:QteCloseRevision, cf:QteCloseSubcategory, cf:ApptCategory, cf:ApptGlobalObjectId, cf:ApptIsAllDayEvent, cf:ApptImportSequenceNumber, cf:ApptOutlookOwnerApptId, cf:ApptOverriddenCreatedOn, cf:ApptSubcategory, cf:ApptSubscriptionId, cf:ApptLocation, cf:ActualCost, cf:CampActImportSequenceNumber, cf:BudgetedCost, cf:ActualCost, cf:IgnoreInactiveListMembers, cf:DoNotSendOnOptOut, cf:TypeCode, cf:CampActSubcategory, cf:CampActOverriddenCreatedOn, cf:ExcludeIfContactedInXDays, cf:CampActCategory, cf:BudgetedCost, cf:CampActChannelTypeCode, cf:FirstName, cf:ReceivedOn, cf:ResponseCode, cf:YomiLastName, cf:CampResOverriddenCreatedOn, cf:YomiFirstName, cf:CompanyName, cf:CampResCategory, cf:Telephone, cf:OriginatingActivityId, cf:Fax, cf:LastName, cf:CampResImportSequenceNumber, cf:OriginatingActivityIdTypeCode, cf:EMailAddress, cf:CampResChannelTypeCode, cf:YomiCompanyName, cf:PromotionCodeName, cf:CampResSubcategory, cf:SuccessCount, cf:OperationTypeCode, cf:BulkOperationNumber, cf:TargetMembersCount, cf:CreatedRecordTypeCode, cf:Parameters, cf:ErrorNumber, cf:TargetedRecordTypeCode, cf:FailureCount, cf:Compressed, cf:ReadReceiptRequested, cf:DeliveryReceiptRequested, cf:EmailSubcategory, cf:Notifications, cf:MessageId, cf:Sender, cf:ToRecipients, cf:EmailOverriddenCreatedOn, cf:SubmittedBy, cf:EmailImportSequenceNumber, cf:EmailDirectionCode, cf:MimeType, cf:MessageIdDupCheck, cf:DeliveryAttempts, cf:TrackingToken, cf:EmailCategory, cf:SvcApptImportSequenceNumber, cf:SvcApptLocation, cf:SvcApptIsAllDayEvent, cf:TimeSpent, cf:CompetitorId, cf:OppCloseOverriddenCreatedOn, cf:OppCloseImportSequenceNumber, cf:ActualRevenue, cf:ActualRevenue, cf:OppCloseSubcategory, cf:OppCloseCategory, cf:IsMapiPrivate, cf:LeftVoiceMail, cf:EmailAttachmentCount, cf:ConversationIndex, cf:InReplyTo, cf:CorrelationMethod, cf:BaseConversationIndexHash, cf:ParentActivityId, cf:SenderMailboxId, cf:DeliveryLastAttemptedOn, cf:StageId, cf:SentOn, cf:DeliveryPriorityCode, cf:PostponeActivityProcessingUntil, cf:ProcessId, cf:PostMessageType, cf:ImportSequenceNumber, cf:InResponseTo, cf:PostAuthor, cf:PostedOn, cf:OverriddenCreatedOn, cf:ThreadId, cf:SocialAdditionalParams, cf:PostURL, cf:PostFromProfileId, cf:SocialActivityDirectionCode, cf:PostId, cf:SentimentValue, cf:PostAuthorAccount, cf:PostToProfileId, cf:PostAuthorAccountName, cf:PostAuthorAccountType, cf:PostAuthorType, cf:PostAuthorName, cf:PostAuthorYomiName, cf:PostAuthorAccountYomiName, cf:Community, cf:EmailSender, cf:SendersAccount, cf:EmailSenderObjectTypeCode, cf:EmailSenderName, cf:SendersAccountName, cf:SendersAccountObjectTypeCode, cf:EmailSenderYomiName, cf:SendersAccountYomiName') 
TBLPROPERTIES ('hbase.table.name' = 'ActivityPointerBase');