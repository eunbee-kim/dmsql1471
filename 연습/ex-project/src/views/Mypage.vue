<template>
  <div>
    <h1>{{ userinfo.first_name + " " + userinfo.last_name }} 님 환영합니다.</h1>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  computed: {
    ...mapState(["userinfo"]),
  },
};
</script>