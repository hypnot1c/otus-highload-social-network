CREATE TABLE "User" (
  "Id"            SERIAL NOT NULL CONSTRAINT "PK_User_Id" PRIMARY KEY,
  "PublicId"      uuid NOT NULL,
  "PasswordHash"  varchar(300)
);

CREATE UNIQUE INDEX UIX_User_PublicId ON "User" ("PublicId");