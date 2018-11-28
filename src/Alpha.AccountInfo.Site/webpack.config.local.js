const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const merge = require('webpack-merge');
const common = require('./webpack.config.js');

module.exports = merge(common, {
    mode: 'local',
    output: {
        path: path.resolve(__dirname, 'Features'),
        filename: '[name].min.js'
    },
    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",
    module: {
        rules: [
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            {
                enforce: "pre",
                test: /\.js$/,
                loader: "source-map-loader"
            }
        ]
    },
    optimization: {
        minimizer: [
            new OptimizeCSSAssetsPlugin({})
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "[name].min.css",
            chunkFilename: "[id].min.css"
        })
    ]
});