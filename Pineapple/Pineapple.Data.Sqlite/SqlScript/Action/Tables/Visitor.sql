Create table if not exists Visitor
 (
     VisitorId     			TEXT PRIMARY KEY,
     FirstVisitTime    		TEXT,
	 EnterUrl				TEXT,
	 RefererUrl				TEXT,
	 UserName				TEXT
 )