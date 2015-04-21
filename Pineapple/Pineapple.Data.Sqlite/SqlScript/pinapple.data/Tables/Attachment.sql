Create table if not exists Attachment
(
    AttachmentId    INTEGER primary key AUTOINCREMENT,
    OriginName      TEXT,
    FileName        TEXT,
    Path            TEXT,
    ContentType     TEXT,
    Type            TEXT,
    Size            INT,
    Alt             TEXT,
    Version         INT
)