CREATE TABLE "Post" (
  "Id"             SERIAL NOT NULL CONSTRAINT "PK_Post_Id" PRIMARY KEY,
  "PublicId"       UUID NOT NULL,
  "Author_UserId"  INTEGER NOT NULL,
  "Text"           TEXT NOT NULL,
  "CreatedAt"      TIMESTAMP NOT NULL,
  "ModifiedAt"     TIMESTAMP NOT NULL,

  CONSTRAINT "FK_Post_Author_UserId_To_User_Id" FOREIGN KEY ("Author_UserId") REFERENCES "User" ("Id")
);

CREATE UNIQUE INDEX UIX_Post_PublicId ON "Post" ("PublicId");
