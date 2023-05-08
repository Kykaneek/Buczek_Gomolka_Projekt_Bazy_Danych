CREATE or alter VIEW PlannedTracesListItemView AS
SELECT 
	l.ID loadingID,
	l.pickupdate,
	l.time_to_loading,
	t.distance,
	t.travel_time,
	co.name Contractor,
	lo.Name Start_location,
	loc.Name Finish_location,
	UL.time_to_unloading,
	u.login Driver,
	u.phone,
	c.buy_date,
	c.registration_number
FROM Loading L
	JOIN Trace T ON 
		T.ID=L.TraceID
	JOIN Contractors CO ON 
		t.contractor_id = cO.ID
	JOIN Location LO ON t.Start_location = lo.ID
	JOIN Location LOC ON t.Finish_location = loc.ID
	JOIN UnLoading UL ON UL.loading_ID = L.ID
	JOIN Cars C ON l.carID = C.ID
	JOIN Users U on c.driver = u.login
