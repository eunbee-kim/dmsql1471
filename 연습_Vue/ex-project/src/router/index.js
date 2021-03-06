import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);
const rejectAuthUser = (to, from, next) => {
  if (localStorage.getItem("access_token") !== null) {
    //이미 로그인 된 유저니까 막아야한다
    alert("이미 로그인을 하였습니다")
    next('/')
  } else {
    next()
  }
}

const onlyAuthUser = (to, from, next) => {
  if (localStorage.getItem("access_token") === null) {
    //아직 로그인이 안된 유저니까 막아야한다
    alert("로그인이 필요한 기능입니다")
    next('/login')
  } else {
    next()
  }
}
const routes = [
  {
    path: "/",
    name: "Home",
    component: () =>
      import(/* webpackChunkName: "Home" */ "../views/Home.vue")
  },
  {
    path: "/login",
    name: "login",
    beforeEnter: rejectAuthUser,
    component: () =>
      import(/* webpackChunkName: "Home" */ "../views/Login.vue")
  },
  {
    path: "/mypage",
    name: "mypage",
    beforeEnter: onlyAuthUser,
    component: () =>
      import(/* webpackChunkName: "Home" */ "../views/Mypage.vue")
  },
  {
    path: "/was",
    name: "was",
    beforeEnter: onlyAuthUser,
    component: () =>
      import(/* webpackChunkName: "Home" */ "../views/WAS.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
