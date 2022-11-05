<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer">
      <!--  -->
    </v-navigation-drawer>

    <v-app-bar>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>Application</v-toolbar-title>
    </v-app-bar>

    <v-main>
      <div contenteditable="false">
        <svg
          version="1.1"
          xmlns="http://www.w3.org/2000/svg"
          xmlns:xlink="http://www.w3.org/1999/xlink"
          xmlns:xml="http://www.w3.org/XML/1998/namespace"
          width="500"
          height="500"
          viewBox="-250, -250, 500, 500"
        >
          <g>
            <circle r="100" stroke-width="2" style="fill: Red; stroke: Black" />
            <foreignObject
              v-if="isInEditMode"
              x="20"
              y="20"
              width="160"
              height="160"
            >
              <div
                contenteditable="true"
                xmlns="http://www.w3.org/1999/xhtml"
                @input.stop.prevent="onDivInput($event)"
                style="width: 100%; height: 100%; white-space: pre-wrap"
                v-html="text"
              />
            </foreignObject>
            <text v-else x="20" y="20" width="160" height="160">
              <tspan
                x="0"
                dy="1.5em"
                v-for="(textLine, l) in textLines"
                :key="l"
              >
                {{ textLine }}
              </tspan>
            </text>
          </g>
        </svg>
      </div>
      <div style="white-space: pre-wrap">{{ text }}</div>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { computed, ref } from "vue";

const text = ref("aaaaa\nbbbbbb\ncccccc");
const drawer = ref<boolean | null>(null);
const isInEditMode = ref(false);

const textLines = computed(() => text.value.split("\n"));

function onDivInput(e: Event) {
  console.log(e);
}
</script>
