# WASB variables
$storageAccountName = "crmhbasestorage"
$containerName = "crmhbasecluster2"

# Local paths
$workflowDefinition = "C:\Users\t-ankigu\Documents\Visual Studio 2013\Projects\ConsoleApplication2\ConsoleApplication2\workflow.xml"
$coordDefinition =  "C:\Users\t-ankigu\Documents\Visual Studio 2013\Projects\ConsoleApplication2\ConsoleApplication2\coordinator.xml"

$joinActivityPhone = "C:\Users\t-ankigu\Documents\Visual Studio 2013\Projects\ConsoleApplication2\ConsoleApplication2\join_activity_phone.hql"


# Wasb dest folder
$destFolder = "oozie"

# Cluster credentials
$storageaccountkey = get-azurestoragekey $storageAccountName | %{$_.Primary}
$destContext = New-AzureStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $storageaccountkey


# Upload Files
Write-Host "Copy workflow definition, HiveQL, and Coordinator script file ..." -ForegroundColor Green

Set-AzureStorageBlobContent -File $coordDefinition -Container $containerName -Blob "$destFolder/coordinator.xml" -Context $destContext
Set-AzureStorageBlobContent -File $workflowDefinition -Container $containerName -Blob "$destFolder/workflow.xml" -Context $destContext

Set-AzureStorageBlobContent -File $opportunityScript -Container $containerName -Blob "$destFolder/opportunityquery.hql" -Context $destContext
Set-AzureStorageBlobContent -File $accountScript -Container $containerName -Blob "$destFolder/accountquery.hql" -Context $destContext
Set-AzureStorageBlobContent -File $leadScript -Container $containerName -Blob "$destFolder/leadquery.hql" -Context $destContext
Set-AzureStorageBlobContent -File $contactScript -Container $containerName -Blob "$destFolder/contactquery.hql" -Context $destContext

Set-AzureStorageBlobContent -File $joinActivityPhone -Container $containerName -Blob "$destFolder/join_activity_phone.hql" -Context $destContext

