#  BEGIN DEBUG support functions
# ===========================================================================================================
# Specific debug helpers adjust where and how you need it
#
#
# Debug for 3.3 (See CMakeList.txt)
#   Call: debug_copy_headers("${GENERATED_H_DEST}" "${GENERATED_HEADERS}")
function(debug_copy_headers GENERATED_H_DEST GENERATED_HEADERS)
    message("\nDEBUG (Copy headers)\n======================================================")
    message(" - maker path: ${GENERATED_H_DEST}")
    message(" - ${CMAKE_COMMAND} -E make_directory ${GENERATED_H_DEST}")
    message(" - ${CMAKE_COMMAND} -E copy_if_different ${GENERATED_HEADERS} ${GENERATED_H_DEST}")
    message(" - ${CMAKE_COMMAND} -E touch ${GENERATED_H_DEST}/copied.marker")
    message("END DEBUG (Copy headers)\n======================================================\n")
endfunction()

# END Debug functions
# ===========================================================================================================
