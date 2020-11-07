FROM node:lts-alpine

WORKDIR /app
ENV PATH /node_modules/.bin:$PATH

RUN yarn global add eslint @angular/cli

COPY ["package.json", "yarn.lock", "./"]
RUN npm install

COPY . .

EXPOSE 4200
ENTRYPOINT ["ng", "serve", "--host", "0.0.0.0", "--disable-host-check", "--watch"]