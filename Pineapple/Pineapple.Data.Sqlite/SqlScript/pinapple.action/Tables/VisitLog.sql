Create table if not exists VisitLog
 (
     VisitorId     			TEXT,
	 VisitTime				TEXT,
	 EnterUrl				TEXT,
	 RefererUrl				TEXT,
	 UserName				TEXT,
	 ClientIp				TEXT,
	 UserAgent				TEXT,
	 VisitTimeInMs			INTEGER
 );