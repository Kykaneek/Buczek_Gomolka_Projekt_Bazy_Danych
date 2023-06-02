CREATE or alter VIEW OrdersListItemView AS

select ca.registration_number registation
,U.login driver
,lo.Name startloc
,lo2.Name finishloc
,co.name contractor
,CONVERT(VARCHAR(10), l.pickupdate, 105) pickupdates
,l.time_to_loading loading
,ul.time_to_unloading unloading
	FROM Loading L 
	JOIN UnLoading UL on l.ID = ul.loading_ID
	JOIN Contractors CO on co.ID = l.ContractorID 
	JOIN Trace T on t.ID = l.TraceID
	JOIN Cars CA on ca.ID = l.carID
	JOIN Users U on CA.driver = u.ID
	JOIN Location LO on lo.ID = T.Startlocation
	JOIN Location LO2 on lo2.ID = T.Finishlocation