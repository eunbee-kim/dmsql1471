<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link>
    </div>
    <router-view />
    <DxChart :data-source="data">
      <DxArgumentAxis :tick-interval="10" />
      <DxSeries type="bar" />
      <DxLegend :visible="false" />
    </DxChart>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>

<script>
import DxChart, {
  DxArgumentAxis,
  DxSeries,
  DxLegend,
} from "devextreme-vue/chart";
const data = [
  {
    arg: 1990,
    val: 5320816667,
  },
  {
    arg: 2000,
    val: 6127700428,
  },
  {
    arg: 2010,
    val: 6916183482,
  },
];
export default {
  components: {
    DxChart,
    DxArgumentAxis,
    DxSeries,
    DxLegend,
  },
  data() {
    return {
      data,
    };
  },
};
</script>