# Docker Setup

## Start Docker on Windows by opening Docker Desktop

## Check if Docker is running
``` docker info ```

``` docker --version ```

## Check what containers are running
``` docker ps -a ```

## Start Ollama Docker Container First Time Run
``` docker run --name ollamaLocal --hostname=ollamaLocal --env=CUDA_VISIBLE_DEVICES=0 --env=GPU_MEMORY_UTILIZATION=90 --env=GPU_LAYERS=35 --env=NVIDIA_DRIVER_CAPABILITIES=compute,utility --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin --env=OLLAMA_HOST=0.0.0.0 --env=LD_LIBRARY_PATH=/usr/local/nvidia/lib:/usr/local/nvidia/lib64 --env=NVIDIA_VISIBLE_DEVICES=all --volume=ollama:/root/.ollama --network=bridge -p 11434:11434 --restart=no --label='org.opencontainers.image.ref.name=ubuntu' --label='org.opencontainers.image.version=22.04' --runtime=runc -d sha256:f1fd985cee59a6403508e3ba26367744eafad7b7383ba960d80872aae61661b6 ```

### Following Runs
``` docker stop ollamaLocal```

```docker start ollamaLocal```

## Start Postgres with pgvector Container
``` docker run -d --name postgres_with_pgvector -e POSTGRES_PASSWORD=password99 -e POSTGRES_USER=postgres -e POSTGRES_DB=vectordb -p 5432:5432 --restart unless-stopped -v pgvector_data:/var/lib/postgresql/data pgvector/pgvector:pg16 ```

### Following Runs
```docker stop postgres__with_pgvector```

```docker start postgres__with_pgvector```