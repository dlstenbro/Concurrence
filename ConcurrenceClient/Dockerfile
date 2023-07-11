#stage 1
ARG ENV_DEPLOYMENT_TYPE
ARG CONCURRENCE_API_HOSTNAME
ARG CONCURRENCE_API_PORT

FROM node:latest as builder

#redefine build variables here
# otherwise it will show blank during docker build stage
ARG ENV_DEPLOYMENT_TYPE
ENV DEPLOYMENT_TYPE=${ENV_DEPLOYMENT_TYPE}

WORKDIR /app
COPY . .

RUN npm cache clean --force
RUN npm install -g npm@latest
RUN npm install --save --legacy-peer-deps && npm install -g @angular/cli
RUN npm run build -- --configuration=${DEPLOYMENT_TYPE}

#stage 2
FROM nginx:alpine
COPY --from=builder /app/dist/concurrence-client /usr/share/nginx/html
EXPOSE 80/TCP
