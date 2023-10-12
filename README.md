# OTUS Highload Architect course homework

 Репозиторий для домашних заданий по курсу

## Инструкция по запуску

Подразумевается что перед запуском уже установлен docker/docker desktop

1. Клонировать репозиторий / скачать
2. В файле .env указать пути местам хранения БД - PG_MASTER_DATA_PATH, PG_SLAVE_1_DATA_PATH, PG_SLAVE_2_DATA_PATH
3. Открыть терминал (cmd, ps etc.) и перейти в корень репозитория
4. Выполнить команду:
    > docker-compose up -d
5. Выполснить команды:
    > docker exec -ti postgres_db_master su postgres -c "mkdir /var/lib/postgresql/data/pgslave"
      docker exec -ti postgres_db_master su postgres -c "pg_basebackup --username=replication_user --pgdata=/var/lib/postgresql/data/pgslave --wal-method=stream --write-recovery-conf"
      docker stop postgres_db_slave_1
      docker stop postgres_db_slave_2
6. Перенести содержимое каталога PG_MASTER_DATA_PATH/pgslave в PG_SLAVE_1_DATA_PATH и PG_SLAVE_2_DATA_PATH
7. Заменить строку primary_conninfo в postgresql.auto.conf на slave1
    > primary_conninfo = 'user=replication_user host=postgres_db_master port=5432 application_name=walreceiver password=PASSWORD channel_binding=prefer sslmode=prefer sslcompression=0 sslsni=1 ssl_min_protocol_version=TLSv1.2 gssencmode=prefer krbsrvname=postgres target_session_attrs=any'
8. Заменить строку primary_conninfo в postgresql.auto.conf на slave2
    > primary_conninfo = 'user=replication_user host=postgres_db_master port=5432 application_name=walreceiversync password=PASSWORD channel_binding=prefer sslmode=prefer sslcompression=0 sslsni=1 ssl_min_protocol_version=TLSv1.2 gssencmode=prefer krbsrvname=postgres target_session_attrs=any'
9. Выполнить команды:
    > docker start postgres_db_slave_2
      docker start postgres_db_slave_1



### Postman

1. Импортировать в Postman переменные окружения и коллекцию запросов из папки /postman
2. Для окружения DEV указать значение переменной OTUS.HS.SN.Web.Api значением http://localhost:PORT, где PORT - значение порта для http (не https) из контейнера с приложением "otus.ha.sn.web.api"
3. Выполнить HTTP запрос Users/Register из коллекции
4. Полученный user_id и указанный при запросе пароль указать в качестве параметров тела в запросе Login/Login
5. Полученный token указать в качестве Bearer token авторизации (настроена на уровне папки Social network)
6. Теперь можно выполнять запросы Users/Get by id
