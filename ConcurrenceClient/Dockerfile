#stage 1
ARG DEPLOYMENT_TYPE

FROM node:latest as builder

ARG ENV_DEPLOYMENT_TYPE
ENV DEPLOYMENT_TYPE=${ENV_DEPLOYMENT_TYPE}

WORKDIR /app
COPY . .
RUN npm install && npm install -g @angular/cli
CMD npm run build -- --configuration=${DEPLOYMENT_TYPE}

#stage 2
ARG DEPLOYMENT_TYPE

FROM nginx:alpine

ARG ENV_DEPLOYMENT_TYPE
ENV DEPLOYMENT_TYPE=${ENV_DEPLOYMENT_TYPE}

COPY --from=builder /app/dist/concurrence-client /usr/share/nginx/html
EXPOSE 80/TCP
