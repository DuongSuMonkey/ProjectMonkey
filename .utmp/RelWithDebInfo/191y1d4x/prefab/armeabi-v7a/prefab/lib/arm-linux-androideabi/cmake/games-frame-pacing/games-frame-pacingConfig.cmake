if(NOT TARGET games-frame-pacing::swappy)
add_library(games-frame-pacing::swappy SHARED IMPORTED)
set_target_properties(games-frame-pacing::swappy PROPERTIES
    IMPORTED_LOCATION "D:/Gradle8/gradle-8.0.1-all/gradle-8.0.1/caches/transforms-3/82fff1c468b0b4659b7be4540e50f25c/transformed/jetified-games-frame-pacing-1.10.0/prefab/modules/swappy/libs/android.armeabi-v7a_API22_NDK23_cpp_shared_Release/libswappy.so"
    INTERFACE_INCLUDE_DIRECTORIES "D:/Gradle8/gradle-8.0.1-all/gradle-8.0.1/caches/transforms-3/82fff1c468b0b4659b7be4540e50f25c/transformed/jetified-games-frame-pacing-1.10.0/prefab/modules/swappy/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

if(NOT TARGET games-frame-pacing::swappy_static)
add_library(games-frame-pacing::swappy_static STATIC IMPORTED)
set_target_properties(games-frame-pacing::swappy_static PROPERTIES
    IMPORTED_LOCATION "D:/Gradle8/gradle-8.0.1-all/gradle-8.0.1/caches/transforms-3/82fff1c468b0b4659b7be4540e50f25c/transformed/jetified-games-frame-pacing-1.10.0/prefab/modules/swappy_static/libs/android.armeabi-v7a_API22_NDK23_cpp_shared_Release/libswappy.a"
    INTERFACE_INCLUDE_DIRECTORIES "D:/Gradle8/gradle-8.0.1-all/gradle-8.0.1/caches/transforms-3/82fff1c468b0b4659b7be4540e50f25c/transformed/jetified-games-frame-pacing-1.10.0/prefab/modules/swappy_static/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

