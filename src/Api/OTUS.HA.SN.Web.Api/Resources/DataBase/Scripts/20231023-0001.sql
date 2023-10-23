CREATE TABLE "User_Dialog" (
  "Id"             SERIAL NOT NULL CONSTRAINT "PK_User_Dialog_Id" PRIMARY KEY,
  "From_UserId"    INTEGER NOT NULL,
  "To_UserId"      INTEGER NOT NULL,
  "Text"           TEXT NOT NULL,
  "CreatedAt"      TIMESTAMP NOT NULL,

  CONSTRAINT "FK_User_Dialog_From_UserId_To_User_Id" FOREIGN KEY ("From_UserId") REFERENCES "User" ("Id"),
  CONSTRAINT "FK_User_Dialog_To_UserId_To_User_Id" FOREIGN KEY ("To_UserId") REFERENCES "User" ("Id")
);
