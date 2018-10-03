const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const merge = require('webpack-merge');
const common = require('./webpack.config.js');

module.exports = merge(common, {
    output: {
        path: path.resolve(__dirname, 'assets'),
        filename: '[name].js'
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",
    plugins: [
        new MiniCssExtractPlugin({
            filename: "[name].css",
            chunkFilename: "bundles[id].css"
        }),
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: JSON.stringify('development')
            }
        })
    ]
});