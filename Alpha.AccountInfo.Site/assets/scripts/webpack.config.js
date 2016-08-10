module.exports = {
    resolve: {
        extensions: ['', '.ts', '.tsx', '.js'],
    },
    entry: {
        accountInfoApp: "./account-info-app.tsx"
    },
    output: {
        filename: "../bundles/[name].js"
    },

    module: {
        loaders: [
            { test: /\.tsx?$/, loader: 'ts-loader?jsx=true', exclude: /(\.test.ts$|node_modules)/ },
        ],
    },

    externals: {
        "react": "React",
        "react-dom": "ReactDOM",
        "redux": "Redux",
        "react-redux": "ReactRedux",
        "jquery": "jQuery",
        "lodash": "_"
    },
}