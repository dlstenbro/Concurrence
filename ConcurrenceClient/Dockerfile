#stage 1
FROM node:latest as builder

#redefine build variables here
# otherwise it will show blank during docker build stage
ARG ENV_DEPLOYMENT_TYPE
ARG ENV_CONCURRENCE_API_HOSTNAME
ARG ENV_CONCURRENCE_API_PORT

ENV DEPLOYMENT_TYPE=${ENV_DEPLOYMENT_TYPE}
ENV CONCURRENCE_API_HOSTNAME=${ENV_CONCURRENCE_API_HOSTNAME}
ENV CONCURRENCE_API_PORT=${ENV_CONCURRENCE_API_PORT}

WORKDIR /app
COPY . .

RUN npm install -g npm@latest @angular/cli && \
    npm run build -- --configuration=${DEPLOYMENT_TYPE}

#stage 2
FROM nginx:alpine
COPY --from=builder /app/dist/concurrence-client /usr/share/nginx/html
EXPOSE 80/TCP
