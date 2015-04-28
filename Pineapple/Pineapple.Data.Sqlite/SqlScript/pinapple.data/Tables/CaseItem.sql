Create table if not exists CaseItem
(
    CaseItemId	    INTEGER primary key AUTOINCREMENT,
    CaseId	        Integer,
    Title           TEXT,
    AttachmentId    INTEGER,
    DisplayOrder    INTEGER,
    CreateDate		TEXT
)