CREATE TABLE "User" (
  "Id"            SERIAL NOT NULL,
  "PublicId"      uuid NOT NULL,
  "Firstname"     varchar(100) NOT NULL,
  "Secondname"    varchar(200) NOT NULL,
  "BirthDate"     date,
  "Biography"     varchar(5000),
  "City"          varchar(100),
  "PasswordHash"  varchar(300)
);
