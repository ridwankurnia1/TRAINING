export interface Truck {
  truckId?: number;
  truckName?: string;
  merk?: string;
  driver?: string;
  joinDate?: Date;
  endDate?: Date;
  createdDate?: Date;
  createdBy?: Date;
  updatedDate?: Date;
  updatedBy?: Date;
  recordStatus?: number;
  
  //Temporary, untuk nampung di excel
  tempStatus?: string;
  tempJoinDate?: string;
  tempEndDate?: string;
}
