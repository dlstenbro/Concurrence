const webpack = require('webpack');

module.exports = {
  plugins: [
    new webpack.DefinePlugin({
      $ENV: {
        DEPLOYMENT_TYPE: JSON.stringify(process.env.DEPLOYMENT_TYPE),
        CONCURRENCE_API_PORT: JSON.stringify(process.env.CONCURRENCE_API_PORT),
        CONCURRENCE_API_HOSTNAME: JSON.stringify(process.env.CONCURRENCE_API_HOSTNAME)
      }
    })
  ]
};
