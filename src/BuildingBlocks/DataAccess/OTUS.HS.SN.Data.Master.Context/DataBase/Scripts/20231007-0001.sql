CREATE TABLE "User" (
  "Id"            SERIAL NOT NULL CONSTRAINT "PK_User_Id" PRIMARY KEY,
  "PublicId"      uuid NOT NULL,
  "Firstname"     varchar(100) NOT NULL,
  "Secondname"    varchar(200) NOT NULL,
  "BirthDate"     date,
  "Biography"     varchar(5000),
  "City"          varchar(100)
);

CREATE UNIQUE INDEX UIX_User_PublicId ON "User" ("PublicId");