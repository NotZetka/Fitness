FROM node:20.18 as build

WORKDIR /app/client

COPY package*.json .

RUN npm ci

RUN npm install -g @angular/cli

COPY . .

RUN npm run build --configuration=production

FROM nginx:latest

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/client/dist/client/browser /usr/share/nginx/html

EXPOSE 4200