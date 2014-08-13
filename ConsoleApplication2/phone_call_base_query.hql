SET hbase.zookeeper.quorum=zookeepernode0,zookeepernode1,zookeepernode2;
INSERT OVERWRITE DIRECTORY '/PhoneCallBaseQuery' 
select activityid,mcs_dialstarttime, mcs_Duration, mcs_HoldTime from phonecallbase  
