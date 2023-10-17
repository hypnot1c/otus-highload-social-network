CREATE TABLE "Friends" (
  "FriendOne_UserId" INTEGER NOT NULL,
  "FriendTwo_UserId" INTEGER NOT NULL,
  
  CONSTRAINT "PK_Friends_FriendOne_UserId_FriendTwo_UserId" PRIMARY KEY ("FriendOne_UserId", "FriendTwo_UserId"),
  CONSTRAINT "FK_Friends_FriendOne_UserId_To_User_Id" FOREIGN KEY ("FriendOne_UserId") REFERENCES "User" ("Id"),
  CONSTRAINT "FK_Friends_FriendTwo_UserId_To_User_Id" FOREIGN KEY ("FriendTwo_UserId") REFERENCES "User" ("Id")
);
