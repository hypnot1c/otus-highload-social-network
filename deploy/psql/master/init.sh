#!/bin/bash
set -e

cat /tmp/postgresql.conf > /var/lib/postgresql/data/postgresql.conf
cat /tmp/pg_hba.conf > /var/lib/postgresql/data/pg_hba.conf

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE DATABASE otus_social_network;
	CREATE USER web_api WITH ENCRYPTED PASSWORD 'password';

	GRANT ALL PRIVILEGES ON DATABASE otus_social_network TO web_api;

	GRANT CONNECT ON DATABASE otus_social_network TO web_api;

	GRANT USAGE ON SCHEMA public TO web_api;

	GRANT pg_read_server_files TO web_api;

	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO web_api;
EOSQL

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "otus_social_network" <<-EOSQL
	GRANT ALL ON SCHEMA public TO web_api;
EOSQL

pgbench -i -U web_api otus_social_network

# Replication
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE ROLE replication_user WITH LOGIN PASSWORD 'PASSWORD' REPLICATION;
	GRANT CONNECT ON DATABASE otus_social_network TO replication_user;
EOSQL

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "otus_social_network" <<-EOSQL
	GRANT SELECT ON ALL TABLES IN SCHEMA public TO replication_user;
	GRANT SELECT ON ALL SEQUENCES IN SCHEMA public TO replication_user;
	GRANT USAGE ON SCHEMA public TO replication_user;
	ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO replication_user;
EOSQL
