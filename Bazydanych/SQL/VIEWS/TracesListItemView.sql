CREATE OR ALTER VIEW OrdersListItemView
AS
select LO.Name startlocation,LOC.Name finishlocation,CO.name contractor,L.pickupdate,l.time_to_loading loading,UL.time_to_unloading unloading,T.distance from Loading L 
JOIN Trace T on L.TraceID = T.ID 
JOIN UnLoading UL on L.ID = UL.loading_ID 
JOIN Location LO on  T.Start_location = LO.ID
JOIN Location LOC on T.Finish_location = LOC.ID
JOIN Cars C ON L.carID = C.ID 
JOIN Contractors CO on CO.ID = T.contractor_id
