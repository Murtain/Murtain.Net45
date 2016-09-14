# Amaze UI React

[Amaze UI][amazeui] components build with React.

基于 [Amaze UI][amazeui] 和 React.js 封装的 Web 组件库。

[![Bower version](https://img.shields.io/bower/v/amazeui-react.svg?style=flat-square)](https://github.com/amazeui/amazeui-react)
[![NPM version](https://img.shields.io/npm/v/amazeui-react.svg?style=flat-square)](https://www.npmjs.com/package/amazeui-react)
[![Build Status](https://img.shields.io/travis/amazeui/amazeui-react.svg?style=flat-square)](https://travis-ci.org/amazeui/amazeui-react)
[![Dependency Status](https://img.shields.io/david/amazeui/amazeui-react.svg?style=flat-square)](https://david-dm.org/amazeui/amazeui-react)
[![devDependency Status](https://img.shields.io/david/dev/amazeui/amazeui-react.svg?style=flat-square)](https://david-dm.org/amazeui/amazeui-react#info=devDependencies)

**规范及工具**：

- [React 编码规范](https://github.com/Minwe/style-guide/blob/master/React.js.md)
- [React JetBrains 编辑器 Live Templates](https://github.com/Minwe/jetbrains-react)

## 开发及构建

### 目录结构

```
├── package.json
├── dist          # UMD 包构建目录
├── docs          # 文档源文件
├── examples      # 示例源文件
├── lib           # npm 包构建目录
├── www           # 文档构建目录
└── src           # 组件源文件（JSX）
```

### 开发

使用之前先安装相关依赖：

```
npm install gulp -g && npm install
```

- 开发及文档

  ```
  npm start
  ```

- 构建

  ```
  npm run build
  ```

- 示例

  ```
  npm run examples
  ```

[amazeui]: https://github.com/allmobilize/amazeui
