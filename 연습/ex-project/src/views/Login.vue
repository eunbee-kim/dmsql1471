<template>
  <v-container fill-height style="max-width: 450px">
    <v-layout align-center row wrap>
      <v-flex xs12>
        <v-alert dense outlined type="error" :value="isLoginError">
          <strong>아이디와 비밀번호를 확인해주세요.</strong>
        </v-alert>

        <v-alert dense outlined type="success" :value="isLogin">
          <strong>로그인이 완료되었습니다.</strong>
        </v-alert>

        <v-card dark hover>
          <v-toolbar flat>
            <v-toolbar-title>로그인</v-toolbar-title>
          </v-toolbar>
          <div class="pa-3">
            <v-text-field v-model="email" label="Email" outlined></v-text-field>
            <v-text-field
              v-model="password"
              label="Password"
              outlined
              type="password"
            ></v-text-field>
            <v-btn
              block
              depressed
              large
              color="grey darken-3"
              @click="login({ email, password })"
              >로그인</v-btn
            >
          </div>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      email: null,
      password: null,
    };
  },
  computed: {
    ...mapState(["isLogin", "isLoginError"]),
  },

  methods: {
    ...mapActions(["login"]),
    //전체 유저에서 해당 이메일로 유저를 찾는다
    /*       let selectedUser = null;
      this.allUsers.forEach((user) => {
        if (user.email === this.email) selectedUser = user;
      });

      selectedUser === null
        ? (this.isError = true)
        : selectedUser.password !== this.password
        ? (this.isError = true)
        : ((this.isSuccess = true), (this.isError = false)); */
    //그 유저의 비밀번호와 입력된 비밀번호를 비교한다
    //},
  },
};
</script>