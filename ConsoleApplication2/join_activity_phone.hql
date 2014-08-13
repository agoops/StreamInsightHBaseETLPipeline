set mapred.child.java.opts=Xmx512M;
set hive.execution.engine=tez;
SET hbase.zookeeper.quorum=zookeepernode0,zookeepernode1,zookeepernode2;
INSERT OVERWRITE DIRECTORY '/JoinQuery' 
select pcb.*, apb.RegardingObjectIdName, apb.OwnerId, apb.PhoneSubcategory, apb.PhoneCategory, apb.PhoneNumber  
from (select * from phonecallbase) pcb join (select * from activitypointerbase limit 500000) apb  on pcb.activityid = apb.activityid;
