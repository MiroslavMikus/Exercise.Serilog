## Docker cheatsheet

### Pull kibana and elasticsearch images

```powershell
docker pull docker.elastic.co/kibana/kibana:6.6.1
docker pull docker.elastic.co/elasticsearch/elasticsearch:6.6.1
```

#### Run without docker-compose

```powershell
docker run --name elasticsearch -h elasticsearch -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:6.6.1 
docker run --name kibana -h kibana -p 5601:5601 --link elasticsearch:elasticsearch docker.elastic.co/kibana/kibana:6.6.1
```

#### Run with docker compose

**Start**
```powershell
docker-compose -f elastic-compose.yml up
```

**Stop**

```powershell
docker-compose -f elastic-compose.yml down
```

### Open elastic

1. Check elasticsearch at http://localhost:9200/
2. Check kibana status at http://localhost:5601/status
3. Open kibana at http://localhost:5601/app/kibana