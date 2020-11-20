<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>
      For a guide and recipes on how to configure / customize this project,<br />
      check out the
      <a href="https://cli.vuejs.org" target="_blank" rel="noopener"
        >vue-cli documentation</a
      >.
    </p>
    <h3>Installed CLI Plugins</h3>

    <h3>Essential Links</h3>

    <h3>Ecosystem</h3>
    <Module1 />
    <Module2 />
    <router-view />
  </div>
</template>

<script>
import Module1 from "./Module1";
import Module2 from "./Module2";

export default {
  name: "HelloWorld",
  props: {
    msg: String,
  },
  components: {
    Module1,
    Module2,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
