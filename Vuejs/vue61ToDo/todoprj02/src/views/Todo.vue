<style scoped>
body {
  text-align: center;
  background-color: #f6f6f8;
}
input {
  border-style: groove;
  width: 200px;
}
button {
  border-style: groove;
}
.shadow {
  box-shadow: 5px 10px 10px rgba(0, 0, 0, 0.03);
}
</style>

<template>
  <div id="app">
    <TodoHeader></TodoHeader>
    <TodoInput v-on:addTodo="addTodo"></TodoInput>
    <TodoList
      v-bind:propsdata="todoItems"
      v-on:removeTodo="removeTodo"
    ></TodoList>
    <TodoFooter v-on:removeAll="clearAll"></TodoFooter>
  </div>
</template>


<script>
import TodoHeader from "@/components/todo/TodoHeader.vue";
import TodoInput from "@/components/todo/TodoInput.vue";
import TodoList from "@/components/todo/TodoList.vue";
import TodoFooter from "@/components/todo/TodoFooter.vue";

export default {
  data: function() {
    return {
      todoItems: ["영화보기", "주말 산책", "ES6 학습", "잠실 야구장"]
    };
  },
  //template: ``,
  methods: {
    addTodo: function(todoItem) {
      //this.$data.todoItems.push(todoItem);
      //this.$data.todoItems[this.$data.todoItems.length] = todoItem;
      this.$set(this.$data.todoItems, this.$data.todoItems.length, todoItem);
    },
    removeTodo: function(todoItem, index) {
      this.$data.todoItems.splice(index, 1);
    },
    clearAll: function() {
      //this.$data.todoItems = [];
      this.$set(this.$data, "todoItems", []);
    }
  },
  components: {
    TodoHeader: TodoHeader,
    TodoInput: TodoInput,
    TodoList: TodoList,
    TodoFooter: TodoFooter
  }
};
</script>