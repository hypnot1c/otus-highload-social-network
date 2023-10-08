CREATE INDEX 
  "User_Firstname_Secondname_Lower" 
ON 
"User" 
(Lower("Firstname") varchar_pattern_ops, Lower("Secondname") varchar_pattern_ops);
