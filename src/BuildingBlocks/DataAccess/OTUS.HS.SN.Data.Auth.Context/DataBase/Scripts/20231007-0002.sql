COPY 
  "User" ("PublicId", "PasswordHash") 
FROM 
  '/docker-entrypoint-initdb.d/user_data_auth.copy'