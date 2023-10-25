CREATE TABLE "User_Dialog" (
  "Id"             SERIAL NOT NULL,
  "From_UserId"    INTEGER NOT NULL,
  "To_UserId"      INTEGER NOT NULL,
  "Text"           TEXT NOT NULL,
  "CreatedAt"      TIMESTAMP NOT NULL,

  CONSTRAINT "PK_User_Dialog_Id_From_UserId_To_UserId" PRIMARY KEY ("Id", "From_UserId", "To_UserId")
);
