<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <v-list-item to="/"> Home</v-list-item>
      <!-- <v-list-item v-if="isLogin === false" to="/login"> 로그인</v-list-item> 
      <v-list-item v-else to="/mypage">마이페이지</v-list-item>-->
      <v-list-item v-if="isLogin === true" to="/was"
        >Web Server / WAS</v-list-item
      >
      <v-list-item>HTTP</v-list-item>
      <v-list-item>TCP/IP</v-list-item>
      <v-list-item>MVC</v-list-item>
      <v-list-item>DB 정규화</v-list-item>
      <v-list-item>교육 일정 보기</v-list-item>
    </v-navigation-drawer>
    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>AD SOFT 교육과정</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-menu offset-y v-if="isLogin">
          <template v-slot:activator="{ on, attrs }">
            <v-btn depressed v-bind="attrs" v-on="on">Menu</v-btn>
          </template>
          <v-list>
            <v-list-item to="/mypage">
              <v-list-item-title>마이페이지</v-list-item-title>
            </v-list-item>

            <v-list-item @click="logout">
              <v-list-item-title>로그아웃</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-btn depressed v-if="isLogin === false" router :to="{ name: 'login' }"
          >Log in</v-btn
        >
      </v-toolbar-items>
    </v-app-bar>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  data: () => ({ drawer: null }),
  computed: {
    ...mapState(["isLogin"]),
  },
  methods: {
    ...mapActions(["logout"]),
  },
};
</script>